using System;
using System.Threading.Tasks;
using Domain.Models.Survey;
using Telegram.Bot.Types;

namespace API.Services.BotServices.MessageService
{
    public interface IMessageService
    {
        ValueTask HandleUpdateAsync(Update update);
        ValueTask SendSurveyAsync(SurveyToGroup survey);
        ValueTask CloseSurveyPollsAsync(Guid surveyId);
    }
}
