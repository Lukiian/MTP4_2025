using CSharpToJsonSchema;
using System.ComponentModel;

namespace MTP4_2025.Functions;

[GenerateJsonSchema(GoogleFunctionTool = true)]
public interface ICurrencyChangeFunction
{
    [Description("Convert amount between currencies")]
    CurrencyResult ConvertCurrency(
        decimal amount,
        [Description("ISO code, e.g. 'USD'")] string from,
        [Description("ISO code, e.g. 'UAH'")] string to);
}

public class CurrencyChangeFunction : ICurrencyChangeFunction
{
    public CurrencyResult ConvertCurrency(decimal amount, string from, string to)
    {
        var rate = 42.7m;
        var converted = (from, to) switch
        {
            ("USD", "UAH") => amount * rate,
            ("UAH", "USD") => amount / rate,
            _ => amount
        };
        return new CurrencyResult()
        {
            Amount = amount,
            From = from,
            To = to,
            Converted = converted
        };
    }
}

public class CurrencyResult
{
    public decimal Amount { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Converted { get; set; }
}