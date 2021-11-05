using API.Constants;
using Domain.Models;
using Domain.Repositories.BotRepository;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Services.BotServices
{
    public class BotUpdateService
    {
        private readonly ITelegramBotClient botClient;
        private readonly IBotRepository botRepository;
        private bool isAuthorized;

        public BotUpdateService(ITelegramBotClient botClient, IBotRepository botRepository)
        {
            this.botClient = botClient;
            this.botRepository = botRepository;
        }

        public async Task HandleUpdate(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => MessageReceived(update.Message),
                _ => botClient.SendTextMessageAsync(update.Message.Chat.Id, "default")
            };

            await handler;
        }

        private async Task MessageReceived(Message message)
        {
            var action = message.Text switch
            {
                "/start" => Authorize(message.Chat.Id),
                _ => ParseMessage(message)
            };
        }

        private async Task Authorize(long chatId)
        {
            var fleg = botRepository.CheckIfAuthorized(chatId);
            await botClient.SendTextMessageAsync(chatId, BotConstants.AuthorizationText);
        }

        private async Task ParseMessage(Message message)
        {
            var data = message.Text.Split("\n");
            var student = new Student
            {
                Id = message.Chat.Id,
                FirstName = data[0],
                LastName = data[1],
                Group = new Group { Number = long.Parse(data[2]) }
            };
            await botRepository.AddStudent(student);
            await botClient.SendTextMessageAsync(message.Chat.Id, BotConstants.DataSaved);
        }

    }
}