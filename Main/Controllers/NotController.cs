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

        [Route("Not/{title}-{description}-{delay}-{iterations}")]
        [HttpPost]
        public IActionResult Post([FromRoute] string title, string description, int delay, int iterations)
        {
            TitleAndDesc[0] = title; TitleAndDesc[1] = description;
            NotificationsCount = iterations;
            new Notification(delay);
            return Ok();
        }
    }
}