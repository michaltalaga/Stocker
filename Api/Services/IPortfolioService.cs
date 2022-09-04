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

        /* can't make model binder to work with enums atm. panding microsoft to fix it
        public TransactionType Type { get; set; }
        public DateTimeOffset Date { get; set; }
        */
        public string BuySell { get; set; }
        public string Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerShare { get; set; }
    }
}