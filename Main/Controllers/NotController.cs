using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Main.Properties;

namespace Main.Controllers
{
    [ApiController]
    public class NotController : ControllerBase
    {
        private readonly ILogger<NotController> _logger;

        public NotController(ILogger<NotController> logger)
        {
            _logger = logger;
        }

        [Route("Not/{title}-{description}")]
        [HttpPost]
        public IActionResult Post([FromRoute] string title,[FromRoute] string description)
        {
            TitleAndDesc[0] = title; TitleAndDesc[1] = description;
            new Notification(true);
            return Ok();
        }
    }
}