using Domain.Models.Student;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            var group = await GetGroupAsync(student.GroupNumber);
            group.Students.Add(student);

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

        public async Task UpdateStudentAsync(Student newStudent)
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

        private async Task<Group> GetGroupAsync(long groupNumber)
        {
            var group = await context.Groups.FirstOrDefaultAsync(g => g.Number == groupNumber);
            if (group is null)
                group = (await context.Groups.AddAsync(new Group { Number = groupNumber })).Entity;
            return group;
        }

        public async Task<ICollection<Student>> GetGroupStudentsAsync(long groupNumber) =>
            (await context.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.Number == groupNumber)).Students;
    }
}