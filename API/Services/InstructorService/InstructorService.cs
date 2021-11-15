using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Survey;
using Domain.Repositories.SurveyRepository;

namespace API.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly ISurveyRepository repository;
        private readonly IMapper mapper;

        public InstructorService(ISurveyRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task ChangeSurveyStatusAsync(Guid surveyId, bool isOpened) => await repository.ChangeSurveyStatusAsync(surveyId, isOpened);

        public async Task CreateSurveyAsync(SurveyDto surveyDto) => await repository.AddSurveyAsync(mapper.Map<Survey>(surveyDto));

        public async Task DeleteSurveyAsync(Guid surveyId) => await repository.DeleteSurveyAsync(surveyId);

        public Task<SurveyDto> GetSurveyAsync(Guid surveyId)
        {
            throw new NotImplementedException();
        }
    }
}
