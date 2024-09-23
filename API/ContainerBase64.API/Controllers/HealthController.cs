using Microsoft.AspNetCore.Mvc;

namespace ContainerBase64.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // GET api/health
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("healthy");
        }
    }
}
