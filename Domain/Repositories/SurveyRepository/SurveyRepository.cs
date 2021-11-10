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

        private async Task<(Survey survey, Question question, Answer answer)> GetSurveyTupleAsync(long questionId)
        {
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Id == questionId);
            var survey = await context.Surveys.FirstAsync(s => s.Questions.Contains(question));
            var answer = await context.Answers.FirstOrDefaultAsync(a => a.QuestionId == questionId);
            return (survey, question, answer);
        }

        public async Task<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var survey = await context.Surveys.Include(s => s.Questions).ThenInclude(q => q.Options).FirstAsync(s => s.Id == surveyId);
            return survey.Questions;
        }

        public async Task AddQuestionMessageAsync(long questionId, QuestionMessage message)
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

        public async Task RegisterOptionAnswerAsync(long questionId, string optionText)
        {
            var tuple = await GetSurveyTupleAsync(questionId);
            var option = tuple.question.Options.First(o => o.Text == optionText);
            if (tuple.answer is null)
            {
                tuple.answer = new Answer
                {
                    SurveyId = tuple.survey.Id,
                    QuestionId = tuple.question.Id,
                    StudentId = tuple.question.Messages.First(m => m.MessageId == questionId).StudentId,
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

        public async Task RegisterTextAnswerAsync(long questionId, string text)
        {
            var tuple = await GetSurveyTupleAsync(questionId);
            if (tuple.answer is null)
            {
                tuple.answer = new Answer
                {
                    SurveyId = tuple.survey.Id,
                    QuestionId = tuple.question.Id,
                    StudentId = tuple.question.Messages.First(m => m.MessageId == questionId).StudentId,
                    Text = text
                };
            }
            else
            {
                tuple.answer.Text = text;
            }
            await context.SaveChangesAsync();
        }
    }
}
