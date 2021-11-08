using Application.Constants;
using Domain.Models.Student;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task HandlePollAnswerAsync(Poll poll) =>
            await (poll.TotalVoterCount == 0
            ? surveyRepository.DeleteAnswerAsync(poll.Id)
            : surveyRepository.RegisterAnswerAsync(poll.Id, poll.Options.First(o => o.VoterCount == 1).Text));


        public async Task HandleTextMessageAsync(Message message)
        {
            var action = message.Text switch
            {
                "/start" => GetDataAsync(message.Chat.Id, false),
                "Update data" => GetDataAsync(message.Chat.Id, true),
                _ => ParseDataAsync(message)
            };
            await action;
        }

        public async Task SendSurveyAsync(Survey surveyDto)
        {
            var studentIds = surveyDto.StudentIds is null
                ? await studentRepository.GetGroupStudentsIdsAsync(surveyDto.GroupNumber)
                : surveyDto.StudentIds;

            var survey = new Survey { Id = surveyDto.Id, GroupNumber = surveyDto.GroupNumber, Questions = new List<Question>() };

            foreach (var studentId in studentIds)
            {
                foreach (var question in surveyDto.Questions)
                {
                    var pollOptions = question.Options.Select(o => o.Text);
                    var message = await bot.SendPollAsync(studentId, question.Text, pollOptions, openPeriod: surveyDto.ActivePeriod);

                    var questionOptions = question.Options.Select(o => new Option { Text = o.Text, IsCorrect = o.IsCorrect }).ToList();
                    survey.Questions.Add(new Question { Id = message.Poll.Id, Text = question.Text, Options = questionOptions, StudentId = studentId });
                }
            }

            await surveyRepository.AddSurveyAsync(survey);
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
