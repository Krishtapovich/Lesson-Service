using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Group;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.GroupRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext context;

        public GroupRepository(DataContext context)
        {
            this.context = context;
        }

        public async ValueTask<IEnumerable<string>> GetGroupsNumbersAsync()
        {
            return await context.Groups.Select(g => g.Number).ToListAsync();
        }

        public async ValueTask<IEnumerable<StudentModel>> GetStudentsAsync()
        {
            return await context.Students.ToListAsync();
        }

        public async ValueTask AddStudentAsync(StudentModel student)
        {
            var group = await GetGroupAsync(student.GroupNumber);
            if (group.Students is null)
                group.Students = new List<StudentModel>();
            group.Students.Add(student);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async ValueTask<bool> CheckIfAuthorizedAsync(long studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            return student is not null;
        }

        public async ValueTask DeleteStudentAsync(long studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }

        public async ValueTask UpdateStudentAsync(StudentModel newStudent)
        {
            var student = await context.Students.FindAsync(newStudent.Id);
            var newGroup = await GetGroupAsync(newStudent.GroupNumber);
            student.FirstName = newStudent.FirstName;
            student.LastName = newStudent.LastName;
            student.GroupNumber = newGroup.Number;
            if (newGroup.Students is null)
                newGroup.Students = new List<StudentModel>();
            newGroup.Students.Add(student);
            await context.SaveChangesAsync();
        }

        private async ValueTask<GroupModel> GetGroupAsync(string groupNumber)
        {
            var group = await context.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.Number == groupNumber);
            if (group is null)
                group = (await context.Groups.AddAsync(new GroupModel { Number = groupNumber })).Entity;
            return group;
        }

        public async ValueTask<IEnumerable<StudentModel>> GetGroupStudentsAsync(string groupNumber)
        {
            return await context.Students.Where(s => s.GroupNumber == groupNumber).ToListAsync();
        }
    }
}