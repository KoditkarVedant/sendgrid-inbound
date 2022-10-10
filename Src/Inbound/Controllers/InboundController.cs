using Inbound.Parsers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inbound.Controllers
{
    [Route("/")]
    [ApiController]
    public class InboundController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Process POST from Inbound Parse and print received data.
        [HttpPost]
        [Route("inbound")]
        public async Task<IActionResult> InboundParse()
        {
            var inboundParser = await InboundWebhookParser.Create(Request.Body);

            var inboundEmail = inboundParser.Parse();

            return Ok();
        }
    }
}