namespace Api.Models;

public class Portfolio
{
    public string id => OwnerEmail + ":" + Name;
    public string Name { get; set; }
    public string OwnerEmail { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}

public class Transaction
{
    public string Symbol { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public decimal Quantity { get; set; }
    public decimal PricePerShare { get; set; }
}
public enum TransactionType
{
    Buy,
    Sell
}