using Domain.Models.Survey;
using System.Threading.Tasks;

namespace Domain.Repositories.SurveyRepository
{
    public interface ISurveyRepository
    {
        Task AddSurveyAsync(Survey survey);
        Task RegisterAnswerAsync(string questionId, string optionText);
        Task DeleteAnswerAsync(string questionId);
    }
}
