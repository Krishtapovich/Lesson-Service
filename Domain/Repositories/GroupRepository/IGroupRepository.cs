using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;

namespace Domain.Repositories.GroupRepository
{
    public interface IGroupRepository

    {
        ValueTask<IEnumerable<string>> GetGroupsNumbersAsync();
        ValueTask<IEnumerable<StudentModel>> GetStudentsAsync();
        ValueTask<IEnumerable<StudentModel>> GetGroupStudentsAsync(string groupNumber);
        ValueTask<bool> CheckIfAuthorizedAsync(long studentId);
        ValueTask AddStudentAsync(StudentModel student);
        ValueTask UpdateStudentAsync(StudentModel student);
        ValueTask DeleteStudentAsync(long studentId);
    }
}