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
        public string Name { get; set; }
    }
}