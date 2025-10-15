using GenerativeAI;
using GenerativeAI.Tools;
using GenerativeAI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MTP4_2025.Functions;
using MTP4_2025.Models;
using MTP4_2025.Providers;

namespace MTP4_2025.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIController(IGoogleAIKeyProvider keyProvider, IMemoryCache cache) : ControllerBase
    {
        private const string SystemInstruction = "You are a student of ukrainian university. Please give user an aswer in ukrainian. Prompt: ";
        private const int MaxHistoryMessages = 5;

        [HttpPost]
        [Route("chat")]
        public async Task<ActionResult> Chat(AIModel model)
        {
            var key = keyProvider.GetApiKey();

            var genAi = new GenerativeModel(key, model: "gemini-2.5-flash");

            var weatherFunction = new QuickTool(new WeatherFunction().GetCurrentWeather);
            var currencyChangeFunction = new QuickTool(new CurrencyChangeFunction().ConvertCurrency);
            var productFunction = new QuickTool(new ProductFunction().FindProduct);

            genAi.AddFunctionTool(weatherFunction);
            genAi.AddFunctionTool(currencyChangeFunction);
            genAi.AddFunctionTool(productFunction);
            genAi.EnableFunctions();

            #region Get Conversation History
            var conversationId = model.ConversationId ?? Guid.NewGuid().ToString();
            var cacheKey = $"chat_history_{conversationId}";

            var chatHistory = cache.Get<List<Models.ChatMessage>>(cacheKey) ?? new List<Models.ChatMessage>();
            var chat = genAi.StartChat(chatHistory.Select(msg => new Content
            {
                Role = msg.Role,
                Parts = new List<Part> { new Part { Text = msg.Text } }
            }).ToList());
            #endregion

            var response = await genAi.GenerateContentAsync(model.Message);

            #region Add Conversation History
            chatHistory.Add(new Models.ChatMessage
            {
                Role = "user",
                Text = model.Message
            });
            chatHistory.Add(new Models.ChatMessage
            {
                Role = "model",
                Text = response.Text
            });

            if (chatHistory.Count > MaxHistoryMessages * 2)
            {
                chatHistory = chatHistory.Skip(chatHistory.Count - (MaxHistoryMessages * 2)).ToList();
            }

            cache.Set(cacheKey, chatHistory, TimeSpan.FromHours(1));
            #endregion

            return Ok(new { message = response.Text });
        }
    }
}
