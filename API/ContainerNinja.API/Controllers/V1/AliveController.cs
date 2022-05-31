using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContainerNinja.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AliveController : ControllerBase
    {
        private readonly ILogger<AliveController> _logger;

        public AliveController(ILogger<AliveController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Executing Action AliveController.Get()");

            //return "API is alive!";

            throw new System.Exception("Some Exception");
        }
    }
}