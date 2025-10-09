using GenerativeAI;
using Microsoft.AspNetCore.Mvc;
using MTP4_2025.Models;
using MTP4_2025.Providers;

namespace MTP4_2025.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIController(IGoogleAIKeyProvider keyProvider) : ControllerBase
    {
        private const string SystemInstruction = "You are a student of ukrainian university. Please give user an aswer in ukrainian. Prompt: ";

        [HttpPost]
        [Route("chat")]
        public async Task<ActionResult> Chat(AIModel model)
        {
            var key = keyProvider.GetApiKey();

            var genAi = new GenerativeModel(key, model: "gemini-2.5-flash");

            var prompt = SystemInstruction + model.Message;

            var response = await genAi.GenerateContentAsync(prompt);

            return Ok(new { message = response.Text });
        }
    }
}
