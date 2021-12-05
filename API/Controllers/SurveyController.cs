using System;
using System.Threading.Tasks;
using API.Services.BotServices.MessageService;
using API.Services.SurveyService;
using API.Services.TimerService;
using Domain.Models.Survey;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/survey")]
    public class SurveyController : Controller
    {
        private readonly IMessageService messageService;
        private readonly ITimerService timerService;
        private readonly ISurveyService surveyService;

        public SurveyController(IMessageService messageService, ITimerService timerService, ISurveyService surveyService)
        {
            this.messageService = messageService;
            this.timerService = timerService;
            this.surveyService = surveyService;
        }

        [HttpGet("surveys-list")]
        public async ValueTask<IActionResult> GetSurveysAsync()
        {
            return Ok(await surveyService.GetSurveysAsync());
        }

        [HttpGet("survey-questions")]
        public async ValueTask<IActionResult> GetSurveyQuestionsAsync(Guid surveyId)
        {
            return Ok(await surveyService.GetSurveyQuestionsAsync(surveyId));
        }

        [HttpGet("survey-students")]
        public async ValueTask<IActionResult> GetSurveyStudentsAsync(Guid surveyId)
        {
            return Ok(await surveyService.GetSurveyStudentsAsync(surveyId));
        }

        [HttpGet("survey-answers")]
        public async ValueTask<IActionResult> GetSurveyAnswersAsync(Guid surveyId)
        {
            return Ok(await surveyService.GetSurveyAnswersAsync(surveyId));
        }

        [HttpGet("survey-visualization")]
        public async ValueTask<IActionResult> GetSurveyAnswersVisualizationAsync(Guid surveyId)
        {
            return Ok(await surveyService.GetSurveyAnswersVisualizationAsync(surveyId));
        }

        [HttpGet("survey-csv-answers")]
        public async ValueTask<IActionResult> GetSurveyCsvAnswersAsync(Guid surveyId)
        {
            return Ok(await surveyService.GetSurveyCsvAnswersAsync(surveyId));
        }

        [HttpGet("student-answers")]
        public async ValueTask<IActionResult> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            return Ok(await surveyService.GetStudentAnswersAsync(surveyId, studentId));
        }

        [HttpPost("create-survey")]
        public async ValueTask<IActionResult> CreateSurveyAsync([FromBody] SurveyDto survey)
        {
            return Ok(await surveyService.CreateSurveyAsync(survey));
        }

        [HttpPost("send-survey")]
        public async ValueTask<IActionResult> SendSurveyAsync([FromBody] SurveySendingModel survey)
        {
            await surveyService.ChangeSurveyStatusAsync(survey.Id, true);
            await messageService.SendSurveyAsync(survey);
            if (survey.OpenPeriod is not null)
            {
                await timerService.AddTimerAsync(survey.Id, survey.OpenPeriod.Value);
            }
            return Ok();
        }

        [HttpPut("close-survey")]
        public async ValueTask<IActionResult> CloseSurveyAsync(Guid surveyId)
        {
            await messageService.CloseSurveyPollsAsync(surveyId);
            await surveyService.ChangeSurveyStatusAsync(surveyId, false);
            return Ok();
        }

        [HttpDelete("delete-survey")]
        public async ValueTask<IActionResult> DeleteSurveyAsync(Guid surveyId)
        {
            await messageService.CloseSurveyPollsAsync(surveyId);
            await surveyService.DeleteSurveyAsync(surveyId);
            return Ok();
        }
    }
}
