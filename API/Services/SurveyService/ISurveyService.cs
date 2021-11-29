using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;
using Domain.Models.Survey;

namespace API.Services.SurveyService
{
    public interface ISurveyService
    {
        ValueTask<IEnumerable<SurveyListModel>> GetSurveysAsync();
        ValueTask<IEnumerable<QuestionDto>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask<IEnumerable<StudentModel>> GetSurveyStudentsAsync(Guid surveyId);
        ValueTask<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId);
        ValueTask<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto);
        ValueTask DeleteSurveyAsync(Guid surveyId);
        ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);
    }
}
