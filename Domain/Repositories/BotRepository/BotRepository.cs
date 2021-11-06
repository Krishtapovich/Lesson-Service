using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.BotRepository
{
    public class BotRepository : IBotRepository
    {
        private readonly DataContext context;

        public BotRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            var group = await GetGroupAsync(student);
            group.Students.Add(student);
            student.Group = group;

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfAuthorizedAsync(long studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            return student is not null;
        }

        public async Task DeleteStudentAsync(Student student)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var oldStudent = await context.Students.FindAsync(student.Id);
            var newGroup = await GetGroupAsync(student);
            oldStudent.Group.Students.Remove(oldStudent);
            oldStudent.FirstName = student.FirstName;
            oldStudent.LastName = student.LastName;
            oldStudent.Group = newGroup;
            newGroup.Students.Add(oldStudent);

            await context.SaveChangesAsync();
        }

        private async Task<Group> GetGroupAsync(Student student)
        {
            var group = await context.Groups.Where(g => g.Number == student.Group.Number).FirstOrDefaultAsync();
            if (group is null)
                group = (await context.Groups.AddAsync(student.Group)).Entity;
            return group;
        }
    }
}