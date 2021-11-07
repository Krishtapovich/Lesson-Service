using Application.Bot;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Services.BotServices
{
    public class BotService
    {
        private readonly BotClient botClient;

        public BotService(ITelegramBotClient bot, IStudentRepository studentRepository)
        {
            botClient = new BotClient(bot, studentRepository);
        }

        public async Task HandleUpdateAsync(Update update)
        {
            try
            {
                var handler = update.Type switch
                {
                    UpdateType.Message => botClient.HandleTextMessageAsync(update.Message),
                    _ => botClient.UnknownMessageAsync(update.Message)
                };
                await handler;
            }
            catch
            {
                await botClient.UnknownMessageAsync(update.Message);
            }
        }

        public async Task SendSurveyAsync(Survey survey) => await botClient.SendSurveyAsync(survey);
    }
}