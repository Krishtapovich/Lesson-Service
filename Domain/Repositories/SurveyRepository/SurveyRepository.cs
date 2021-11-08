using Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories.SurveyRepository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly DataContext context;

        public SurveyRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddSurveyAsync(Survey survey)
        {
            await context.Surveys.AddAsync(survey);
            await context.SaveChangesAsync();
        }

        public async Task RegisterAnswerAsync(string questionId, string optionText)
        {
            var question = await context.Questions.Include(q => q.Options).FirstAsync(q => q.Id == questionId);
            var option = question.Options.First(o => o.Text == optionText);
            var survey = await context.Surveys.Where(s => s.Questions.Contains(question)).FirstAsync();
            var answer = new Answer { SurveyId = survey.Id, QuestionId = question.Id, Option = option, StudentId = question.StudentId };
            await context.Answers.AddAsync(answer);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(string questionId)
        {
            var answer = await context.Answers.Where(a => a.QuestionId == questionId).FirstAsync();
            context.Answers.Remove(answer);
            await context.SaveChangesAsync();
        }
    }
}
