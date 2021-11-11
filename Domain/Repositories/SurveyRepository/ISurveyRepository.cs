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
        ValueTask RegisterOptionAnswerAsync(long messageId, string optionText);
        ValueTask RegisterTextAnswerAsync(long messageId, string text);
        ValueTask<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask AddQuestionMessageAsync(long questionId, QuestionMessage message);
    }
}
