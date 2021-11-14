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

        public async Task ChangeSurveyStatusAsync(Guid surveyId, bool isClosed)
        {
            var survey = await context.Surveys.FindAsync(surveyId);
            survey.IsClosed = isClosed;
            await context.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            var survey = await context.Surveys.FindAsync(surveyId);
            context.Surveys.Remove(survey);
            await context.SaveChangesAsync();
        }

        public async ValueTask<bool> GetSurveyStatusAsync(Guid surveyId) => (await context.Surveys.FindAsync(surveyId)).IsClosed;

        public async ValueTask<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var survey = await context.Surveys.Include(s => s.Questions).ThenInclude(q => q.Options).FirstAsync(s => s.Id == surveyId);
            return survey.Questions;
        }

        public async ValueTask AddQuestionMessageAsync(int questionId, QuestionMessage message)
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

        public async ValueTask RegisterAnswerAsync(long messageId, string answerText = null, string optionText = null)
        {
            var questionMessage = await context.QuestionMessages.FirstAsync(qm => qm.MessageId == messageId);
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Messages.Contains(questionMessage));
            var survey = await context.Surveys.FirstAsync(s => s.Questions.Contains(question));
            var answer = await context.Answers.FirstOrDefaultAsync(a => a.QuestionMessage.MessageId == messageId);
            var option = question.Options.First(o => o.Text == optionText);

            if (answer is null)
            {
                answer = new Answer
                {
                    QuestionMessage = questionMessage,
                    Text = answerText,
                    Option = option
                };
                await context.Answers.AddAsync(answer);
            }
            else
            {
                answer.Text = answerText;
                answer.Option = option;
            }
            await context.SaveChangesAsync();
        }
    }
}
