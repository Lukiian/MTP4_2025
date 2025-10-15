using CSharpToJsonSchema;
using System.ComponentModel;

namespace MTP4_2025.Functions;

[GenerateJsonSchema(GoogleFunctionTool = true)]
public interface IWeatherFunctions
{
    [Description("Get current weather in a location")]
    Weather GetCurrentWeather(
        [Description("City, e.g. 'Kyiv, UA'")] string location,
        Unit unit = Unit.Celsius);
}

public class WeatherFunction : IWeatherFunctions
{
    public Weather GetCurrentWeather(string location, Unit unit = Unit.Celsius)
    {
        var descriptions = new[]
        {
            "Sunny",
            "Cloudy",
            "Partly Cloudy",
            "Rainy",
            "Stormy",
            "Snowy",
            "Foggy",
            "Windy"
        };

        return new Weather
        {
            Location = location,
            Temperature = Random.Shared.Next(-20, 35),
            Unit = unit,
            Description = descriptions[Random.Shared.Next(descriptions.Length)]
        };
    }
}

public enum Unit
{
    Celsius,
    Fahrenheit,
    Imperial
}

public class Weather
{
    public string Location { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public Unit Unit { get; set; }
    public string Description { get; set; } = string.Empty;
}