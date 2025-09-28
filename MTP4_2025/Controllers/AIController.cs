using Microsoft.AspNetCore.Mvc;
using MTP4_2025.Models;

namespace MTP4_2025.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIController : ControllerBase
    {
        public AIController()
        {
        }

        [HttpPost]
        [Route("chat")]
        public ActionResult Chat(AIModel model)
        {
            return Ok(new { message = "Hello from AIController!" });
        }
    }
}
