using System.Threading.Tasks;
using API.Services.BotServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace API.Controllers
{
    [Route("bot")]
    public class BotController : Controller
    {
        [HttpPost]
        public async ValueTask<IActionResult> Post([FromServices] IMessageService service, [FromBody] Update update)
        {
            await service.HandleUpdateAsync(update);
            return Ok();
        }
    }
}