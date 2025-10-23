using GenerativeAI;
using GenerativeAI.Tools;
using Microsoft.AspNetCore.Mvc;
using MTP4_2025.Functions;
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

            var weatherFunction = new QuickTool(new WeatherFunction().GetCurrentWeather);
            var productFunction = new QuickTool(new ProductFunction().FindProducts);
            var currencyFunction = new QuickTool(new CurrencyFunction().ChangeCurrency);

            genAi.FunctionTools.Add(weatherFunction);
            genAi.FunctionTools.Add(productFunction);
            genAi.FunctionTools.Add(currencyFunction);

            var prompt = model.Message;

            var response = await genAi.GenerateContentAsync(prompt);

            return Ok(new { message = response.Text });
        }
    }
}
