using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Group;

namespace API.Services.GroupService
{
    public interface IGroupService
    {
        ValueTask<IEnumerable<GroupModel>> GetGroupsAsync(int pageNumber, int pageSize);
        ValueTask<IEnumerable<string>> GetGroupsNumbersAsync();
    }
}
