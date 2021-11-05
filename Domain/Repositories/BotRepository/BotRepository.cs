using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repositories.BotRepository
{
    public class BotRepository : IBotRepository
    {
        private readonly DataContext context;

        public BotRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddStudent(Student student)
        {
            if (!context.Groups.Any(x => x.Number == student.Group.Number))
                await context.Groups.AddAsync(student.Group);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfAuthorized(long studentId)
        {
            var res = context.Students.Any(x => x.Id == studentId);
            return res;
        }

        public async Task DeleteStudent(Student student)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}