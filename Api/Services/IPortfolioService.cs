namespace Api.Services;

public interface IPortfolioService
{
    Task Create(string ownerEmail, CreatePortfolioModel portfolio);
    IEnumerable<Portfolio> Get(string ownerEmail);
    Portfolio Get(string ownerEmail, string name);
    public class CreatePortfolioModel
    {
        public string Name { get; set; }
    }
}