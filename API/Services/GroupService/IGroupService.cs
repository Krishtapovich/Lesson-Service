using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;

namespace API.Services.GroupService
{
    public interface IGroupService
    {
        ValueTask<IEnumerable<string>> GetGroupsNumbersAsync();
        ValueTask<IEnumerable<StudentModel>> GetStudentsAsync();
        ValueTask UpdateStudentAsync(StudentModel student);
        ValueTask DeleteStudentAsync(long studentId);
    }
}
