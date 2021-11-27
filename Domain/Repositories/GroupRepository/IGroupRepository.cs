using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;

namespace Domain.Repositories.GroupRepository
{
    public interface IGroupRepository

    {
        ValueTask<IEnumerable<string>> GetGroupsNumbersAsync();
        ValueTask<IEnumerable<GroupModel>> GetGroupsAsync(int pageNumber, int pageSize);
        ValueTask<IEnumerable<StudentModel>> GetGroupStudentsAsync(string groupNumber);

        ValueTask<bool> CheckIfAuthorizedAsync(long studentId);

        ValueTask AddStudentAsync(StudentModel student);
        ValueTask UpdateStudentAsync(StudentModel student);
        ValueTask DeleteStudentAsync(StudentModel student);
    }
}