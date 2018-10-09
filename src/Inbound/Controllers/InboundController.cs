using Inbound.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public IActionResult InboundParse()
        {
            InboundParser _inboundParser = new InboundParser(Request);

            var keyValues = _inboundParser.KeyValues();

            Log(keyValues);

            return Ok();
        }

        private void Log(IDictionary<string, string> keyValues)
        {
            if(keyValues == null)
            {
                return;
            }
            Console.WriteLine(JsonConvert.SerializeObject(keyValues));
        }
    }
}