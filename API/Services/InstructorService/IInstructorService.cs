using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Student;
using Domain.Models.Survey;

namespace API.Services.InstructorService
{
    public interface IInstructorService
    {
        ValueTask<IEnumerable<Group>> GetGroupsAsync(int pageNumber, int pageSize);
        ValueTask<IEnumerable<SurveyDto>> GetSurveysAsync(int pageNumber, int pageSize);
        ValueTask<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId);
        ValueTask<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto);
        ValueTask DeleteSurveyAsync(Guid surveyId);
        ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);
    }
}
