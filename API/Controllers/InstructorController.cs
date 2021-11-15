using System;
using System.Threading.Tasks;
using API.Services.BotServices.MessageService;
using API.Services.InstructorService;
using API.Services.TimerService;
using Domain.Models.Survey;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/instructor")]
    public class InstructorController : Controller
    {
        private readonly IMessageService botService;
        private readonly ITimerService timerService;
        private readonly IInstructorService instructorService;

        public InstructorController(IMessageService botService, ITimerService timerService, IInstructorService instructorService)
        {
            this.botService = botService;
            this.timerService = timerService;
            this.instructorService = instructorService;
        }

        [HttpPost("create-survey")]
        public async Task<IActionResult> CreateSurveyAsync([FromBody] SurveyDto survey)
        {
            await instructorService.CreateSurveyAsync(survey);
            return Ok();
        }

        [HttpPost("send-survey")]
        public async Task<IActionResult> SendSurveyAsync([FromBody] SurveyToGroup survey)
        {
            await botService.SendSurveyAsync(survey);
            await instructorService.ChangeSurveyStatusAsync(survey.Id, true);
            if (survey.OpenPeriod is not null)
            {
                await timerService.AddTimerAsync(survey.Id, survey.OpenPeriod.Value);
            }
            return Ok();
        }

        [HttpPut("close-survey")]
        public async Task<IActionResult> CloseSurveyAsync(Guid surveyId)
        {
            await instructorService.ChangeSurveyStatusAsync(surveyId, false);
            return Ok();
        }

        [HttpDelete("delete-survey")]
        public async Task<IActionResult> DeleteSurveyAsync(Guid surveyId)
        {
            await instructorService.DeleteSurveyAsync(surveyId);
            return Ok();
        }
    }
}
