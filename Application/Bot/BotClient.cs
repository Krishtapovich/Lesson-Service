using Application.Constants;
using Domain.Models.Student;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
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

        public BotClient(ITelegramBotClient bot, IStudentRepository studentRepository)
        {
            this.bot = bot;
            this.studentRepository = studentRepository;
        }

        public async Task UnknownMessageAsync(Message message) =>
            await bot.SendTextMessageAsync(message.Chat.Id, BotConstants.Unknown, replyToMessageId: message.MessageId);

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

        public async Task SendSurveyAsync(Survey survey)
        {
            var students = await studentRepository.GetGroupStudentsAsync(survey.GroupNumber);

            foreach (var student in students)
                foreach (var question in survey.Questions)
                    await bot.SendTextMessageAsync(student.Id, question.Text, replyMarkup: SetMarkup(question));
        }

        private InlineKeyboardMarkup SetMarkup(Question question) =>
            new InlineKeyboardMarkup(question.Options.Select(o => InlineKeyboardButton.WithCallbackData(o.Text)));
                
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
