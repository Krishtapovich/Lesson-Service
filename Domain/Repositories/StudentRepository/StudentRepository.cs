using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Student;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context)
        {
            this.context = context;
        }

        public async ValueTask<IEnumerable<Group>> GetGroupsAsync(int pageNumber, int pageSize)
        {
            return await context.Groups.Include(g => g.Students)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();
        }

        public async ValueTask AddStudentAsync(Student student)
        {
            var group = await GetGroupAsync(student.GroupNumber);
            group.Students.Add(student);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async ValueTask<bool> CheckIfAuthorizedAsync(long studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            return student is not null;
        }

        public async ValueTask DeleteStudentAsync(Student student)
        {
            throw new System.NotImplementedException();
        }

        public async ValueTask UpdateStudentAsync(Student newStudent)
        {
            var student = await context.Students.FindAsync(newStudent.Id);
            var oldGroup = await GetGroupAsync(student.GroupNumber);
            var newGroup = await GetGroupAsync(newStudent.GroupNumber);
            oldGroup.Students.Remove(student);
            student.FirstName = newStudent.FirstName;
            student.LastName = newStudent.LastName;
            student.GroupNumber = newGroup.Number;
            newGroup.Students.Add(student);
            await context.SaveChangesAsync();
        }

        private async ValueTask<Group> GetGroupAsync(string groupNumber)
        {
            var group = await context.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.Number == groupNumber);
            if (group is null)
                group = (await context.Groups.AddAsync(new Group { Number = groupNumber })).Entity;
            return group;
        }

        public async ValueTask<IEnumerable<Student>> GetGroupStudentsAsync(string groupNumber)
        {
            return await context.Students.Where(s => s.GroupNumber == groupNumber).ToListAsync();
        }
    }
}