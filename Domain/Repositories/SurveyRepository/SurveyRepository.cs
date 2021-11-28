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

        public async ValueTask<IEnumerable<SurveyModel>> GetSurveysAsync(int pageNumber, int pageSize)
        {
            return await context.Surveys.OrderByDescending(s => s.CreationTime)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
        }

        public async ValueTask<SurveyModel> AddSurveyAsync(SurveyModel survey)
        {
            var newSurvey = await context.Surveys.AddAsync(survey);
            await context.SaveChangesAsync();
            return newSurvey.Entity;
        }

        public async ValueTask DeleteSurveyAsync(Guid surveyId)
        {
            var survey = await context.Surveys.FindAsync(surveyId);
            context.Surveys.Remove(survey);
            await context.SaveChangesAsync();
        }



        public async ValueTask<bool> CheckIfSurveyClosedAsync(int messageId)
        {
            var message = await context.QuestionMessages.Include(q => q.Question).FirstOrDefaultAsync(m => m.MessageId == messageId);
            return await CheckIfSurveyClosedAsync(message);
        }

        private async ValueTask<bool> CheckIfSurveyClosedAsync(QuestionMessage message)
        {
            if (message is null)
            {
                throw new ArgumentException("Survey doesn't exist or wrong message is replied");
            }
            var survey = await context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == message.Question.SurveyId);
            if ((bool)survey?.IsClosed)
            {
                var exception = new Exception("Survey is closed, your answer wasn't registered");
                exception.Data.Add("chatId", message.StudentId);
                throw exception;
            }
            else
            {
                return false;
            }
        }

        public async ValueTask<bool> CheckIfSurveyClosedAsync(string pollId)
        {
            var message = await context.QuestionMessages.Include(m => m.Question).FirstOrDefaultAsync(m => m.PollId == pollId);
            return await CheckIfSurveyClosedAsync(message);
        }



        public async ValueTask<ImageModel> GetAnswerImageAsync(int messageId)
        {
            var message = await context.Answers.Include(a => a.Image).FirstOrDefaultAsync(a => a.QuestionMessageId == messageId);
            return message?.Image;
        }

        public async ValueTask<IEnumerable<ImageModel>> GetAnswersImagesAsync(Guid surveyId)
        {
            var messages = await context.QuestionMessages.Include(qm => qm.Question).Where(qm => qm.Question.SurveyId == surveyId).ToListAsync();
            return await context.Answers.Include(a => a.Image)
                                              .Include(a => a.QuestionMessage)
                                              .Where(a => a.Image != null && messages.Contains(a.QuestionMessage))
                                              .Select(a => a.Image)
                                              .ToListAsync();
        }



        public async ValueTask<bool> GetSurveyStatusAsync(Guid surveyId)
        {
            return (await context.Surveys.FindAsync(surveyId)).IsClosed;
        }

        public async ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened)
        {
            var survey = await context.Surveys.FindAsync(surveyId);
            survey.IsClosed = !isOpened;
            await context.SaveChangesAsync();
        }



        public async ValueTask<IEnumerable<QuestionModel>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            return await context.Questions.Include(q => q.Options).Where(q => q.SurveyId == surveyId).ToListAsync();
        }

        public async ValueTask<IEnumerable<QuestionMessage>> GetSurveyOptionQuestionsAsync(Guid surveyId)
        {
            return await context.QuestionMessages.Include(qm => qm.Question)
                                                 .ThenInclude(q => q.Options)
                                                 .Where(qm => qm.Question.SurveyId == surveyId && qm.PollId != null)
                                                 .ToListAsync();
        }

        public async ValueTask AddQuestionMessageAsync(int questionId, QuestionMessage message)
        {
            var question = await context.Questions.Include(q => q.Messages).FirstAsync(q => q.Id == questionId);
            var questionMessage = await context.QuestionMessages.FirstOrDefaultAsync(qm => qm.QuestionId == message.QuestionId &&
                                                                                           qm.StudentId == message.StudentId);
            if (questionMessage is null)
            {
                question.Messages.Add(message);
            }
            else
            {
                questionMessage.MessageId = message.MessageId;
                questionMessage.PollId = message.PollId;
            }

            await context.SaveChangesAsync();
        }



        public async ValueTask<IEnumerable<AnswerModel>> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            return await context.Answers.Where(a => a.SurveyId == surveyId && a.QuestionMessage.StudentId == studentId)
                                        .Include(a => a.Option)
                                        .Include(a => a.Image)
                                        .OrderBy(a => a.QuestionMessage.Id)
                                        .ToListAsync();
        }

        public async ValueTask RegisterAnswerAsync(int messageId, string answerText = null, ImageModel image = null)
        {
            var message = await context.QuestionMessages.FirstAsync(qm => qm.MessageId == messageId);
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Id == message.QuestionId);
            var answer = await context.Answers.Include(a => a.Image).FirstOrDefaultAsync(a => a.QuestionMessage.MessageId == messageId);

            if (answer is null)
            {
                answer = new AnswerModel
                {
                    SurveyId = question.SurveyId,
                    QuestionMessage = message,
                    Text = answerText,
                    Image = image
                };
                await context.Answers.AddAsync(answer);
            }
            else
            {
                answer.Text = answerText;
                if (answer.Image is not null)
                {
                    context.Images.Remove(answer.Image);
                }
                answer.Image = image;
            }
            await context.SaveChangesAsync();
        }

        public async ValueTask RegisterAnswerAsync(string pollId, string optionText)
        {
            var message = await context.QuestionMessages.FirstAsync(qm => qm.PollId == pollId);
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Id == message.QuestionId);
            var answer = await context.Answers.Include(a => a.Image).FirstOrDefaultAsync(a => a.QuestionMessage.PollId == pollId);
            var option = question.Options.First(o => o.Text == optionText);

            if (answer is null)
            {
                answer = new AnswerModel
                {
                    SurveyId = question.SurveyId,
                    QuestionMessage = message,
                    Option = option
                };
                await context.Answers.AddAsync(answer);
            }
            else
            {
                answer.Option = option;
            }
            await context.SaveChangesAsync();
        }


        public async ValueTask DeleteStudentSurveyInfoAsync(long studentId)
        {
            var questionMessages = await context.QuestionMessages.Where(qm => qm.StudentId == studentId).ToListAsync();
            context.QuestionMessages.RemoveRange(questionMessages);
            await context.SaveChangesAsync();
        }
    }
}
