using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.SurveyRepository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly DataContext context;

        public SurveyRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddSurveyAsync(Survey survey)
        {
            await context.Surveys.AddAsync(survey);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            var survey = await context.Surveys.FindAsync(surveyId);
            context.Surveys.Remove(survey);
            await context.SaveChangesAsync();
        }

        public async ValueTask<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var survey = await context.Surveys.Include(s => s.Questions).ThenInclude(q => q.Options).FirstAsync(s => s.Id == surveyId);
            return survey.Questions;
        }

        public async ValueTask AddQuestionMessageAsync(long questionId, QuestionMessage message)
        {
            var question = await context.Questions.Include(q => q.Messages).FirstAsync(q => q.Id == questionId);
            if (question.Messages is null)
            {
                question.Messages = new List<QuestionMessage> { message };
            }
            else
            {
                question.Messages.Add(message);
            }
            await context.SaveChangesAsync();
        }

        public async ValueTask RegisterOptionAnswerAsync(long questionId, string optionText)
        {
            var tuple = await GetSurveyTupleAsync(questionId);
            var option = tuple.question.Options.First(o => o.Text == optionText);
            if (tuple.answer is null)
            {
                tuple.answer = new Answer
                {
                    SurveyId = tuple.surveyId,
                    QuestionMessage = tuple.questionMessage,
                    Option = option
                };
                await context.Answers.AddAsync(tuple.answer);
            }
            else
            {
                tuple.answer.Option = option;
            }
            await context.SaveChangesAsync();
        }

        private async ValueTask<(Guid surveyId, Question question, Answer answer, QuestionMessage questionMessage)> GetSurveyTupleAsync(long messageId)
        {
            var questionMessage = await context.QuestionMessages.FirstAsync(qm => qm.MessageId == messageId);
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Messages.Contains(questionMessage));
            var survey = await context.Surveys.FirstAsync(s => s.Questions.Contains(question));
            var answer = await context.Answers.FirstOrDefaultAsync(a => a.QuestionMessage.MessageId == messageId);
            return (survey.Id, question, answer, questionMessage);
        }

        public async ValueTask RegisterTextAnswerAsync(long messageId, string text)
        {
            var tuple = await GetSurveyTupleAsync(messageId);
            if (tuple.answer is null)
            {
                tuple.answer = new Answer
                {
                    SurveyId = tuple.surveyId,
                    QuestionMessage = tuple.questionMessage,
                    Text = text
                };
                await context.Answers.AddAsync(tuple.answer);
            }
            else
            {
                tuple.answer.Text = text;
            }
            await context.SaveChangesAsync();
        }
    }
}
