using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.CloudStorage;
using Application.Constants;
using Domain.Models.Group;
using Domain.Models.Survey;
using Domain.Repositories.GroupRepository;
using Domain.Repositories.SurveyRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Bot
{
    public class BotClient
    {
        private readonly ITelegramBotClient bot;
        private readonly IGroupRepository groupRepository;
        private readonly ISurveyRepository surveyRepository;
        private readonly ICloudStorage cloud;

        public BotClient(ITelegramBotClient bot, IGroupRepository groupRepository,
            ISurveyRepository surveyRepository, ICloudStorage cloud)
        {
            this.bot = bot;
            this.groupRepository = groupRepository;
            this.surveyRepository = surveyRepository;
            this.cloud = cloud;
        }

        public async ValueTask HandlePollAnswerAsync(Poll poll)
        {
            try
            {
                if (!poll.IsClosed && poll.TotalVoterCount == 1)
                {
                    await surveyRepository.CheckIfSurveyClosedAsync(poll.Id);
                    var text = poll.Options.First(o => o.VoterCount == 1).Text;
                    await surveyRepository.RegisterAnswerAsync(poll.Id, text);
                }
            }
            catch (Exception ex)
            {
                await bot.SendTextMessageAsync(ex.Data["chatId"].ToString(), ex.Message);
            }
        }

        public async ValueTask CloseSurveyPollsAsync(Guid surveyId)
        {
            if (!await surveyRepository.GetSurveyStatusAsync(surveyId))
            {
                var polls = await surveyRepository.GetSurveyOptionQuestionsAsync(surveyId);
                foreach (var poll in polls)
                {
                    await bot.StopPollAsync(poll.StudentId, poll.MessageId);
                }
            }
        }

        private async ValueTask HandleReplyMessageAsync(Message message)
        {
            try
            {
                var question = message.ReplyToMessage;
                await surveyRepository.CheckIfSurveyClosedAsync(question.MessageId);
                if (message.Text is not null && question.Poll is null)
                {
                    await surveyRepository.RegisterAnswerAsync(question.MessageId, answerText: message.Text);
                }
                else if (message.Photo is not null && question.Poll is null)
                {
                    var messageImage = await surveyRepository.GetAnswerImageAsync(question.MessageId);
                    if (messageImage is not null)
                    {
                        await cloud.DeleteImageAsync(messageImage.FileName);
                    }
                    using (var stream = new MemoryStream())
                    {
                        await bot.GetInfoAndDownloadFileAsync(message.Photo[2].FileId, stream);
                        var image = await cloud.UploadImageAsync($"{message.MessageId}.jpg", stream);
                        await surveyRepository.RegisterAnswerAsync(question.MessageId, image: image);
                    }
                }
                else
                {
                    throw new ArgumentException(BotConstants.Unknown);
                }
            }
            catch (ArgumentException ex)
            {
                await bot.SendTextMessageAsync(message.Chat.Id, ex.Message);
            }
            catch (Exception ex)
            {
                await bot.SendTextMessageAsync(ex.Data["chatId"].ToString(), ex.Message);
            }
        }

        public async ValueTask HandleTextMessageAsync(Message message)
        {
            try
            {
                if (message.ReplyToMessage is null)
                {
                    var action = message.Text switch
                    {
                        BotConstants.Start => GetDataAsync(message.Chat.Id, false),
                        BotConstants.Update => GetDataAsync(message.Chat.Id, true),
                        _ => ParseDataAsync(message)
                    };
                    await action;
                }
                else
                {
                    await HandleReplyMessageAsync(message);
                }
            }
            catch (Exception)
            {
                await bot.SendTextMessageAsync(message.Chat.Id, BotConstants.Unknown);
            }
        }

        public async ValueTask SendSurveyToGroupsAsync(SurveySendingModel survey)
        {
            foreach (var group in survey.Groups)
            {
                var students = await groupRepository.GetGroupStudentsAsync(group);
                var questions = await surveyRepository.GetSurveyQuestionsAsync(survey.Id);

                foreach (var student in students)
                {
                    foreach (var question in questions)
                    {
                        var questionMessage = await SendQuestionAsync(student, question, survey.OpenPeriod);
                        await surveyRepository.AddQuestionMessageAsync(question.Id, questionMessage);
                    }

                }
            }
        }

        private async ValueTask<QuestionMessage> SendQuestionAsync(StudentModel student, QuestionModel question, int? openPeriod = null)
        {
            Message message;
            if (question.Options.Any())
            {
                var pollOptions = question.Options.Select(o => o.Text);
                message = await bot.SendPollAsync(student.Id, question.Text, pollOptions, openPeriod: openPeriod);
            }
            else
            {
                message = await bot.SendTextMessageAsync(student.Id, question.Text);
            }
            return new QuestionMessage
            {
                MessageId = message.MessageId,
                QuestionId = question.Id,
                StudentId = student.Id,
                Student = student,
                PollId = message.Poll?.Id
            };
        }

        private async ValueTask GetDataAsync(long chatId, bool isUpdatingData)
        {
            var flag = !isUpdatingData && await groupRepository.CheckIfAuthorizedAsync(chatId);
            await (flag ? bot.SendTextMessageAsync(chatId, BotConstants.Authorized)
                        : bot.SendTextMessageAsync(chatId, BotConstants.ReceiveData));
        }

        private async ValueTask ParseDataAsync(Message message)
        {
            var data = message.Text.Split("\n");
            var student = new StudentModel
            {
                Id = message.Chat.Id,
                FirstName = data[0],
                LastName = data[1],
                GroupNumber = data[2]
            };

            var isAuthorized = await groupRepository.CheckIfAuthorizedAsync(message.Chat.Id);
            if (isAuthorized)
            {
                await groupRepository.UpdateStudentAsync(student);
                await surveyRepository.DeleteStudentSurveyInfoAsync(student.Id);
            }
            else
            {
                await groupRepository.AddStudentAsync(student);
            }

            var replyButton = new ReplyKeyboardMarkup(new KeyboardButton[] { BotConstants.Update }) { ResizeKeyboard = true };
            await bot.SendTextMessageAsync(message.Chat.Id, BotConstants.DataSaved, replyMarkup: replyButton);
        }
    }
}
