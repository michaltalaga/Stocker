namespace Api.Models;

public class Portfolio
{
    [JsonProperty("id")]
    public string Name { get; set; }
    public string OwnerEmail { get; set; }
    public Stock[] Stocks { get; set; }
}

public class Stock
{
    public string Code { get; set; }
    public string Exchange { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public TransactionType Transaction { get; set; }
    public DateTimeOffset Date { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
public enum TransactionType
{
    Buy,
    Sell
}