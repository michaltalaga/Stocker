namespace Api.Services;

public interface IPortfolioService
{
    Task Create(string ownerEmail, CreatePortfolioModel portfolio);
    IEnumerable<Portfolio> Get(string ownerEmail);
    Portfolio Get(string ownerEmail, string name);


    Task AddTransaction(string ownerEmail, string portfolioName, AddTransactionModel transaction);



    public class CreatePortfolioModel
    {
        public string Name { get; set; }
    }
    public class AddTransactionModel
    {
        public string Symbol { get; set; }
        public TransactionType Type { get; set; }
        public DateOnly Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerShare { get; set; }
    }
}