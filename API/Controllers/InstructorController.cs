using System;
using System.Threading.Tasks;
using API.Services.BotServices;
using Domain.Models.Survey;
using Domain.Repositories.SurveyRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/instructor")]
    public class InstructorController : Controller
    {
        private readonly BotService service;
        private readonly ISurveyRepository repository;

        public InstructorController(BotService service, ISurveyRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        [HttpPost("send-survey-to-group")]
        public async Task<IActionResult> SendSurveyToGroupAsync(Guid surveyId, long groupNumber)
        {
            await service.SendSurveyToGroupAsync(surveyId, groupNumber);
            return Ok();
        }

        [HttpPost("create-survey")]
        public async Task<IActionResult> CreateSurveyAsync(Survey survey)
        {
            await repository.AddSurveyAsync(survey);
            return Ok();
        }

    }
}
