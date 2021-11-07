using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Student;

namespace Domain.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task<bool> CheckIfAuthorizedAsync(long studentId);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<ICollection<Student>> GetGroupStudentsAsync(long groupNumber);
    }
}