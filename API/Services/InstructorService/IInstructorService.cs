using System;
using System.Threading.Tasks;
using Domain.Models.Survey;

namespace API.Services.InstructorService
{
    public interface IInstructorService
    {
        Task<SurveyDto> GetSurveyAsync(Guid surveyId);
        Task CreateSurveyAsync(SurveyDto surveyDto);
        Task DeleteSurveyAsync(Guid surveyId);
        Task ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);
    }
}
