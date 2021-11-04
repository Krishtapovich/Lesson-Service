using API.Services.BotServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] BotUpdateService service, [FromBody] Update update)
        {
            await service.EchoAsync(update);

            return Ok();
        }
    }
}