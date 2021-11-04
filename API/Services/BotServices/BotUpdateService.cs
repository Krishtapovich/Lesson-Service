using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace API.Services.BotServices
{
    public class BotUpdateService
    {
        private readonly ITelegramBotClient botClient;

        public BotUpdateService(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }

        public async Task EchoAsync(Update update)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);
        }
    }
}