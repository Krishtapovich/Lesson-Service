using System.Threading.Tasks;
using API.Services.GroupService;
using API.Services.SurveyService;
using Domain.Models.Group;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;
        private readonly ISurveyService surveyService;

        public GroupController(IGroupService groupService, ISurveyService surveyService)
        {
            this.groupService = groupService;
            this.surveyService = surveyService;
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
            //await surveyService.DeleteStudentSurveyInfoAsync(student);
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
