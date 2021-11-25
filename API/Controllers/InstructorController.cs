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
        private readonly IMessageService messageService;
        private readonly ITimerService timerService;
        private readonly IInstructorService instructorService;

        public InstructorController(IMessageService messageService, ITimerService timerService, IInstructorService instructorService)
        {
            this.messageService = messageService;
            this.timerService = timerService;
            this.instructorService = instructorService;
        }

        [HttpGet("groups")]
        public async ValueTask<IActionResult> GetGroupsAsync(int pageNumber, int pageSize)
        {
            return Ok(await instructorService.GetGroupsAsync(pageNumber, pageSize));
        }

        [HttpGet("surveys")]
        public async ValueTask<IActionResult> GetSurveysAsync(int pageNumber, int pageSize)
        {
            return Ok(await instructorService.GetSurveysAsync(pageNumber, pageSize));
        }

        [HttpGet("student-answers")]
        public async ValueTask<IActionResult> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            return Ok(await instructorService.GetStudentAnswersAsync(surveyId, studentId));
        }

        [HttpPost("create-survey")]
        public async ValueTask<IActionResult> CreateSurveyAsync([FromBody] SurveyDto survey)
        {
            return Ok(await instructorService.CreateSurveyAsync(survey));
        }

        [HttpPost("send-survey")]
        public async ValueTask<IActionResult> SendSurveyAsync([FromBody] SurveyToGroups survey)
        {
            await messageService.SendSurveyAsync(survey);
            await instructorService.ChangeSurveyStatusAsync(survey.Id, true);
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
            await instructorService.ChangeSurveyStatusAsync(surveyId, false);
            return Ok();
        }

        [HttpDelete("delete-survey")]
        public async ValueTask<IActionResult> DeleteSurveyAsync(Guid surveyId)
        {
            await messageService.CloseSurveyPollsAsync(surveyId);
            await instructorService.DeleteSurveyAsync(surveyId);
            return Ok();
        }
    }
}
