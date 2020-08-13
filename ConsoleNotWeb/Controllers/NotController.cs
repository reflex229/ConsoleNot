using System.Collections.Generic;
using ConsoleNotServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConsoleNotWeb
{
    [ApiController]
    [Route("notContr")]
    
    public class NotController : ControllerBase
    {
        private readonly ILogger<NotController> _logger;

        public NotController(ILogger<NotController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            Properties.Values = new Dictionary<string, string>
            {
                {"Title", "Title"},
                {"Description", "Description"},
                {"IterationTime", "IterationTime"},
                {"Count", "Count"}
            };
            return Properties.Values;
        }
    }
}