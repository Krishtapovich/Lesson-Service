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

        public async ValueTask<IEnumerable<GroupModel>> GetGroupsAsync(int pageNumber, int pageSize)
        {
            return await groupRepository.GetGroupsAsync(pageNumber, pageSize);
        }

        public async ValueTask<IEnumerable<string>> GetGroupsNumbersAsync()
        {
            return await groupRepository.GetGroupsNumbersAsync();
        }
    }
}
