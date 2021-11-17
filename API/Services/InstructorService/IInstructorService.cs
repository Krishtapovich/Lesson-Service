using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Student;
using Domain.Models.Survey;

namespace API.Services.InstructorService
{
    public interface IInstructorService
    {
        Task<IEnumerable<Group>> GetGroupsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<SurveyDto>> GetSurveysAsync(int pageNumber, int pageSize);
        Task<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId);
        Task CreateSurveyAsync(SurveyDto surveyDto);
        Task DeleteSurveyAsync(Guid surveyId);
        Task ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);
    }
}
