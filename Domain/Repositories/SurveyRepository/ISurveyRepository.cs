using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Survey;

namespace Domain.Repositories.SurveyRepository
{
    public interface ISurveyRepository
    {
        Task AddSurveyAsync(Survey survey);
        Task DeleteSurveyAsync(Guid surveyId);
        Task ChangeSurveyStatusAsync(Guid surveyId, bool isClosed);
        ValueTask<bool> GetSurveyStatusAsync(Guid survey);
        ValueTask RegisterAnswerAsync(long messageId, string answerText = null, string optionText = null);
        ValueTask<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask AddQuestionMessageAsync(int questionId, QuestionMessage message);
    }
}
