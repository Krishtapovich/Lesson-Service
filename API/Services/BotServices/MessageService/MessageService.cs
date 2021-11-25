using System;
using System.Threading.Tasks;
using Application.Bot;
using Application.CloudStorage;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Services.BotServices.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly BotClient botClient;

        public MessageService(ITelegramBotClient bot, IStudentRepository studentRepository, ISurveyRepository surveyRepository, ICloudStorage cloud)
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
                    _ => ValueTask.CompletedTask
                };
                await handler;
            }
            catch (Exception) { }
        }

        public async ValueTask SendSurveyAsync(SurveyToGroups survey)
        {
            await botClient.SendSurveyToGroupsAsync(survey);
        }

        public async ValueTask CloseSurveyPollsAsync(Guid surveyId)
        {
            await botClient.CloseSurveyPollsAsync(surveyId);
        }
    }
}