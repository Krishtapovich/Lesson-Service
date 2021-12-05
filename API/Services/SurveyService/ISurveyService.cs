using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;
using Domain.Models.Results;
using Domain.Models.Survey;

namespace API.Services.SurveyService
{
    public interface ISurveyService
    {
        ValueTask<IEnumerable<SurveyListModel>> GetSurveysAsync();
        ValueTask<IEnumerable<QuestionDto>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask<IEnumerable<StudentModel>> GetSurveyStudentsAsync(Guid surveyId);

        ValueTask<IEnumerable<AnswerDto>> GetSurveyAnswersAsync(Guid surveyId);
        ValueTask<IEnumerable<AnswerVisualizationModel>> GetSurveyAnswersVisualizationAsync(Guid surveyId);
        ValueTask<IEnumerable<CsvModel>> GetSurveyCsvAnswersAsync(Guid surveyId);
        ValueTask<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId);

        ValueTask<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto);

        ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);
        ValueTask DeleteStudentSurveyInfoAsync(StudentModel student);

        ValueTask DeleteSurveyAsync(Guid surveyId);
    }
}
