using Microsoft.Extensions.Options;
using MTP4_2025.Models;

namespace MTP4_2025.Providers;

public class GoogleAIKeyProvider(IOptionsMonitor<GoogleAIOptions> _options) : IGoogleAIKeyProvider
{
    private readonly IOptionsMonitor<GoogleAIOptions> options = _options;

    public string GetApiKey()
    {
        return options.CurrentValue.ApiKey;
    }
}
