using System.Threading.Tasks;
using API.Services.GroupService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet("groups-list")]
        public async ValueTask<IActionResult> GetGroupsAsync(int pageNumber, int pageSize)
        {
            return Ok(await groupService.GetGroupsAsync(pageNumber, pageSize));
        }

        [HttpGet("groups-numbers")]
        public async ValueTask<IActionResult> GetGroupsNumbersAsync()
        {
            return Ok(await groupService.GetGroupsNumbersAsync());
        }
    }
}
