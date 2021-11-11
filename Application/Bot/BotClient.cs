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

        public async ValueTask UnknownMessageAsync(Message message)
        {

        }

        public async ValueTask HandlePollAnswerAsync(Poll poll)
        {
            if (poll.TotalVoterCount == 1)
            {
                var text = poll.Options.First(o => o.VoterCount == 1).Text;
                await surveyRepository.RegisterOptionAnswerAsync(long.Parse(poll.Id), text);
            }
        }

        private async ValueTask HandleReplyMessageAsync(Message message)
        {
            var question = message.ReplyToMessage;
            if (message.Text is not null)
            {
                await surveyRepository.RegisterTextAnswerAsync(question.MessageId, message.Text);
            }
            else
            {

            }
        }

        public async ValueTask HandleTextMessageAsync(Message message)
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
                    var messageId = await SendQuestionAsync(studentId, question);
                    var questionMessage = new QuestionMessage { MessageId = messageId, StudentId = studentId, Question = question };
                    await surveyRepository.AddQuestionMessageAsync(question.Id, questionMessage);
                }
            }
        }

        private async ValueTask<long> SendQuestionAsync(long chatId, Question question, int? openPeriod = null)
        {
            Message message;
            if (question.Options.Any())
            {
                var pollOptions = question.Options.Select(o => o.Text);
                message = await bot.SendPollAsync(chatId, question.Text, pollOptions, openPeriod: openPeriod);
            }
            else
            {
                message = await bot.SendTextMessageAsync(chatId, question.Text);
            }
            return question.Options.Count == 0 ? message.MessageId : long.Parse(message.Poll.Id);
        }

        private async ValueTask GetDataAsync(long chatId, bool isUpdatingData)
        {
            var flag = !isUpdatingData && await studentRepository.CheckIfAuthorizedAsync(chatId);
            await (flag ? bot.SendTextMessageAsync(chatId, BotConstants.Authorized)
                        : bot.SendTextMessageAsync(chatId, BotConstants.ReceiveData));
        }

        private async ValueTask ParseDataAsync(Message message)
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
