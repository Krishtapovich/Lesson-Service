using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;
using Domain.Repositories.GroupRepository;

namespace API.Services.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async ValueTask<IEnumerable<string>> GetGroupsNumbersAsync()
        {
            return await groupRepository.GetGroupsNumbersAsync();
        }

        public async ValueTask<IEnumerable<StudentModel>> GetStudentsAsync()
        {
            return await groupRepository.GetStudentsAsync();
        }

        public async ValueTask UpdateStudentAsync(StudentModel student)
        {
            await groupRepository.UpdateStudentAsync(student);
        }

        public async ValueTask DeleteStudentAsync(long studentId)
        {
            await groupRepository.DeleteStudentAsync(studentId);
        }
    }
}
