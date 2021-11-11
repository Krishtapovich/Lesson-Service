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

        public InstructorController(BotService service, ISurveyRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("send-survey-to-group")]
        public async Task<IActionResult> SendSurveyToGroupAsync(Guid surveyId, long groupNumber)
        {
            await service.SendSurveyToGroupAsync(surveyId, groupNumber);
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
