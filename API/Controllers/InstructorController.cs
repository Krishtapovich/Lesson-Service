using System;
using System.Threading.Tasks;
using API.Services.BotServices;
using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly TimerService timerService;

        public InstructorController(BotService service, ISurveyRepository repository,
            IMapper mapper, TimerService timerService)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.timerService = timerService;
        }

        [HttpPost("send-survey-to-group")]
        public async Task<IActionResult> SendSurveyToGroupAsync(Guid surveyId, long groupNumber, int? openPeriod = null)
        {
            await service.SendSurveyToGroupAsync(surveyId, groupNumber, openPeriod);
            if (openPeriod is not null)
            {
                await repository.ChangeSurveyStatusAsync(surveyId, true);
                await timerService.AddTimerAsync(surveyId, openPeriod.Value);
            }
            return Ok();
        }

        [HttpPost("create-survey")]
        public async Task<IActionResult> CreateSurveyAsync(SurveyDto survey)
        {
            await repository.AddSurveyAsync(mapper.Map<Survey>(survey));
            return Ok();
        }

        [HttpDelete("delete-survey")]
        public async Task<IActionResult> DeleteSurveyAsync(Guid surveyId)
        {
            await repository.DeleteSurveyAsync(surveyId);
            return Ok();
        }
    }
}
