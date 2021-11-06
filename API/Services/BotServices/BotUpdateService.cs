using API.Constants;
using Domain.Models;
using Domain.Repositories.BotRepository;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace API.Services.BotServices
{
    public class BotUpdateService
    {
        private readonly ITelegramBotClient botClient;
        private readonly IBotRepository botRepository;
        private bool isDataReceiving;

        public BotUpdateService(ITelegramBotClient botClient, IBotRepository botRepository)
        {
            this.botClient = botClient;
            this.botRepository = botRepository;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => MessageReceivedAsync(update.Message),
                _ => UnknownMessageAsync(update.Message)
            };

            await handler;
        }

        private async Task UnknownMessageAsync(Message message) =>
            await botClient.SendTextMessageAsync(message.Chat.Id, BotConstants.Unknown, replyToMessageId: message.MessageId);


        private async Task MessageReceivedAsync(Message message)
        {

            var action = message.Text switch
            {
                "/start" => GetDataAsync(message.Chat.Id, false),
                "Update data" => GetDataAsync(message.Chat.Id, true),
                _ => isDataReceiving ? ParseDataAsync(message) : UnknownMessageAsync(message)
            };
            await action;
        }

        private async Task GetDataAsync(long chatId, bool isUpdatingData)
        {
            isDataReceiving = true;
            var flag = !isUpdatingData && await botRepository.CheckIfAuthorizedAsync(chatId);
            await (flag ? botClient.SendTextMessageAsync(chatId, BotConstants.Authorized)
                        : botClient.SendTextMessageAsync(chatId, BotConstants.ReceiveData));
        }

        private async Task ParseDataAsync(Message message)
        {
            var replyButton = new ReplyKeyboardMarkup(new KeyboardButton[] { "Update data" }, resizeKeyboard: true);
            var data = message.Text.Split("\n");
            var student = new Student
            {
                Id = message.Chat.Id,
                FirstName = data[0],
                LastName = data[1],
                Group = new Group { Number = long.Parse(data[2]) }
            };
            var isAuthorized = await botRepository.CheckIfAuthorizedAsync(message.Chat.Id);
            await (isAuthorized ? botRepository.UpdateStudentAsync(student) : botRepository.AddStudentAsync(student));
            await botClient.SendTextMessageAsync(message.Chat.Id, BotConstants.DataSaved, replyMarkup: replyButton);
            isDataReceiving = false;
        }

    }
}