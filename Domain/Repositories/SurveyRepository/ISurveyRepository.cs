using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Survey;

namespace Domain.Repositories.SurveyRepository
{
    public interface ISurveyRepository
    {
        Task AddSurveyAsync(Survey survey);
        Task RegisterOptionAnswerAsync(long questionId, string optionText);
        Task RegisterTextAnswerAsync(long questionId, string text);
        Task<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId);
        Task AddQuestionMessageAsync(long questionId, QuestionMessage message);
    }
}
