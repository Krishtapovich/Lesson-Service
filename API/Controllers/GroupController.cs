using System.Threading.Tasks;
using API.Services.GroupService;
using Domain.Models.Group;
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

        [HttpGet("groups-numbers")]
        public async ValueTask<IActionResult> GetGroupsNumbersAsync()
        {
            return Ok(await groupService.GetGroupsNumbersAsync());
        }

        [HttpGet("students-list")]
        public async ValueTask<IActionResult> GetStudentsAsync()
        {
            return Ok(await groupService.GetStudentsAsync());
        }

        [HttpPut("update-student")]
        public async ValueTask<IActionResult> UpdateStudentAsync([FromBody] StudentModel student)
        {
            await groupService.UpdateStudentAsync(student);
            return Ok();
        }

        [HttpDelete("delete-student")]
        public async ValueTask<IActionResult> DeleteStudentAsync(long studentId)
        {
            await groupService.DeleteStudentAsync(studentId);
            return Ok();
        }
    }
}
