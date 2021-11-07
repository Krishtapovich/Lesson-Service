using API.Services.BotServices;
using Domain.Models.Survey;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendSurvey([FromServices] BotService service, [FromBody] Survey survey)
        {
            await service.SendSurveyAsync(survey);

            return Ok();
        }
    }
}
