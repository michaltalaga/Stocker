namespace Api.Models;

public class Portfolio
{
    public string id => OwnerEmail + ":" + Name;
    public string Name { get; set; }
    public string OwnerEmail { get; set; }
    public Transaction[] Transactions { get; set; }
}

public class Transaction
{
    public string Code { get; set; }
    public string Exchange { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public TransactionType Type { get; set; }
    public DateTimeOffset Date { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
public enum TransactionType
{
    Buy,
    Sell
}