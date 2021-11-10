using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Constants;
using Domain.Models.Student;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Bot
{
    public class BotClient
    {
        private readonly ITelegramBotClient bot;
        private readonly IStudentRepository studentRepository;
        private readonly ISurveyRepository surveyRepository;

        public BotClient(ITelegramBotClient bot, IStudentRepository studentRepository, ISurveyRepository surveyRepository)
        {
            this.bot = bot;
            this.studentRepository = studentRepository;
            this.surveyRepository = surveyRepository;
        }

        public async Task UnknownMessageAsync(Message message)
        {

        }

        public async Task HandlePollAnswerAsync(Poll poll)
        {
            if (poll.TotalVoterCount == 1)
            {
                await surveyRepository.RegisterOptionAnswerAsync(long.Parse(poll.Id), poll.Options.First(o => o.VoterCount == 1).Text);
            }
        }

        private async Task HandleReplyMessageAsync(Message message)
        {
            if (message.Photo is null)
            {
                await surveyRepository.RegisterTextAnswerAsync(message.MessageId, message.Text);
            }
            else
            {

            }
        }

        public async Task HandleTextMessageAsync(Message message)
        {
            if (message.ReplyToMessage is null)
            {
                var action = message.Text switch
                {
                    "/start" => GetDataAsync(message.Chat.Id, false),
                    "Update data" => GetDataAsync(message.Chat.Id, true),
                    _ => ParseDataAsync(message)
                };
                await action;
            }
            else
            {
                await HandleReplyMessageAsync(message);
            }
        }

        public async Task SendSurveyToGroupAsync(Guid surveyId, long groupNumber)
        {
            var studentIds = await studentRepository.GetGroupStudentsIdsAsync(groupNumber);
            var questions = await surveyRepository.GetSurveyQuestionsAsync(surveyId);

            foreach (var studentId in studentIds)
            {
                foreach (var question in questions)
                {
                    var message = await SendQuestionAsync(studentId, question);
                    var messageId = question.Options.Count == 0 ? message.MessageId : long.Parse(message.Poll.Id);
                    var questionMessage = new QuestionMessage { MessageId = messageId, StudentId = studentId };
                    await surveyRepository.AddQuestionMessageAsync(question.Id, questionMessage);
                }
            }
        }

        private Task<Message> SendQuestionAsync(long chatId, Question question, int? openPeriod = null)
        {
            if (question.Options.Any())
            {
                var pollOptions = question.Options.Select(o => o.Text);
                return bot.SendPollAsync(chatId, question.Text, pollOptions, openPeriod: openPeriod);
            }
            else
            {
                return bot.SendTextMessageAsync(chatId, question.Text);
            }
        }

        private async Task GetDataAsync(long chatId, bool isUpdatingData)
        {
            var flag = !isUpdatingData && await studentRepository.CheckIfAuthorizedAsync(chatId);
            await (flag ? bot.SendTextMessageAsync(chatId, BotConstants.Authorized)
                        : bot.SendTextMessageAsync(chatId, BotConstants.ReceiveData));
        }

        private async Task ParseDataAsync(Message message)
        {
            var data = message.Text.Split("\n");
            var student = new Student
            {
                Id = message.Chat.Id,
                FirstName = data[0],
                LastName = data[1],
                GroupNumber = long.Parse(data[2])
            };

            var isAuthorized = await studentRepository.CheckIfAuthorizedAsync(message.Chat.Id);
            await (isAuthorized ? studentRepository.UpdateStudentAsync(student) : studentRepository.AddStudentAsync(student));

            var replyButton = new ReplyKeyboardMarkup(new KeyboardButton[] { "Update data" }, resizeKeyboard: true);
            await bot.SendTextMessageAsync(message.Chat.Id, BotConstants.DataSaved, replyMarkup: replyButton);
        }
    }
}
