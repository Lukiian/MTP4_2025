using CSharpToJsonSchema;
using System.ComponentModel;

namespace MTP4_2025.Functions;

[GenerateJsonSchema(GoogleFunctionTool = true)]
public interface ICurrencyFunction
{
    [Description("Convert currency from one to another")]
    CurrencyResult ChangeCurrency(
        decimal amount,
        [Description("ISO code, e.g. 'USD'")] string from,
        [Description("ISO code, e.g. 'UAH'")] string to);
}

public class CurrencyFunction : ICurrencyFunction
{
    public CurrencyResult ChangeCurrency(decimal amount, string from, string to)
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
