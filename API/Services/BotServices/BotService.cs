using System;
using System.Threading.Tasks;
using Application.Bot;
using Application.Cloud;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Services.BotServices
{
    public class BotService
    {
        private readonly BotClient botClient;

        public BotService(ITelegramBotClient bot, IStudentRepository studentRepository,
            ISurveyRepository surveyRepository, IImageCloud cloud)
        {
            botClient = new BotClient(bot, studentRepository, surveyRepository, cloud);
        }

        public async ValueTask HandleUpdateAsync(Update update)
        {
            try
            {
                var handler = update.Type switch
                {
                    UpdateType.Message => botClient.HandleTextMessageAsync(update.Message),
                    UpdateType.Poll => botClient.HandlePollAnswerAsync(update.Poll),
                    _ => botClient.UnknownMessageAsync(update.Message)
                };
                await handler;
            }
            catch (Exception ex)
            {
                await botClient.UnknownMessageAsync(update.Message);
            }
        }

        public async Task SendSurveyToGroupAsync(Guid surveyId, long groupNumber, int? openPeriod = null) =>
            await botClient.SendSurveyToGroupAsync(surveyId, groupNumber, openPeriod);
    }
}