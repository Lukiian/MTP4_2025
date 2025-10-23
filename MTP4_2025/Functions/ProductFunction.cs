using CSharpToJsonSchema;
using System.ComponentModel;

namespace MTP4_2025.Functions;

[GenerateJsonSchema(GoogleFunctionTool = true)]
public interface IProductFunction
{
    [Description("Search products by keywords with optional price filtering")]
    ProductResult[] FindProducts(
        [Description("Product keywords like 'headphones', 'chair'")] string query,
        [Description("Maximum price in USD (optional)")] decimal? maxPrice = null,
        int max = 5);
}

public class ProductFunction : IProductFunction
{
    public ProductResult[] FindProducts(string query, decimal? maxPrice = null, int max = 5)
    {
        var results = Db.Where(p =>
            p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(query, StringComparison.OrdinalIgnoreCase));

        if (maxPrice.HasValue)
        {
            results = results.Where(p => p.Price <= maxPrice.Value);
        }

        return results.Take(max).ToArray();
    }

    private static readonly ProductResult[] Db = {
    new() {
        Name = "Wireless Bluetooth Headphones",
        Description = "Premium noise-cancelling headphones with 30-hour battery life",
        Price = 149.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Stainless Steel Water Bottle",
        Description = "Insulated 32oz bottle keeps drinks cold for 24 hours",
        Price = 34.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Ergonomic Office Chair",
        Description = "Adjustable lumbar support with breathable mesh back",
        Price = 299.00m,
        Currency = "USD"
    },
    new()
    {
        Name = "Mechanical Gaming Keyboard",
        Description = "RGB backlit with blue switches and programmable keys",
        Price = 89.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Yoga Mat Premium",
        Description = "6mm thick non-slip exercise mat with carrying strap",
        Price = 45.00m,
        Currency = "USD"
    },
    new()
    {
        Name = "Smart Watch Fitness Tracker",
        Description = "Heart rate monitor, GPS, and sleep tracking capabilities",
        Price = 199.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Portable Phone Charger",
        Description = "20,000mAh power bank with fast charging support",
        Price = 39.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Coffee Maker Programmable",
        Description = "12-cup drip coffee maker with thermal carafe",
        Price = 79.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "LED Desk Lamp",
        Description = "Adjustable brightness with USB charging port",
        Price = 29.99m,
        Currency = "USD"
    },
    new()
    {
        Name = "Wireless Gaming Mouse",
        Description = "High-precision sensor with customizable DPI settings",
        Price = 59.99m,
        Currency = "USD"
    }
};
}

public class ProductResult
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
}
