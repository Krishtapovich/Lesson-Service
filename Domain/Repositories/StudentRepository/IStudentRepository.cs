using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Student;

namespace Domain.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        ValueTask<IEnumerable<Group>> GetGroupsAsync(int pageNumber, int pageSize);
        ValueTask<IEnumerable<Student>> GetGroupStudentsAsync(long groupNumber);

        ValueTask<bool> CheckIfAuthorizedAsync(long studentId);

        ValueTask AddStudentAsync(Student student);
        ValueTask UpdateStudentAsync(Student student);
        ValueTask DeleteStudentAsync(Student student);
    }
}