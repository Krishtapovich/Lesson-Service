using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;
using Domain.Models.Survey;

namespace Domain.Repositories.SurveyRepository
{
    public interface ISurveyRepository
    {
        ValueTask<IEnumerable<SurveyModel>> GetSurveysAsync();
        ValueTask<SurveyModel> AddSurveyAsync(SurveyModel survey);
        ValueTask DeleteSurveyAsync(Guid surveyId);

        ValueTask<bool> CheckIfSurveyClosedAsync(int messageId);
        ValueTask<bool> CheckIfSurveyClosedAsync(string pollId);

        ValueTask<ImageModel> GetAnswerImageAsync(int messageId);
        ValueTask<IEnumerable<ImageModel>> GetAnswersImagesAsync(Guid surveyId);

        ValueTask<bool> GetSurveyStatusAsync(Guid survey);
        ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);

        ValueTask<IEnumerable<QuestionModel>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask<IEnumerable<QuestionMessage>> GetSurveyOptionQuestionsAsync(Guid surveyId);
        ValueTask AddQuestionMessageAsync(int questionId, QuestionMessage message);

        ValueTask<IEnumerable<StudentModel>> GetSurveyStudentsAsync(Guid surveyId);
        ValueTask<IEnumerable<AnswerModel>> GetSurveyAnswersAsync(Guid surveyId);
        ValueTask<IEnumerable<AnswerModel>> GetStudentAnswersAsync(Guid surveyId, long studentId);
        ValueTask RegisterAnswerAsync(int messageId, string answerText = null, ImageModel image = null);
        ValueTask RegisterAnswerAsync(string pollId, string optionText);

        ValueTask DeleteStudentSurveyInfoAsync(long studentId);
    }
}
