namespace Api.Services;

public interface IPortfolioService
{
    Task Create(string ownerEmail, CreatePortfolioModel portfolio);
    IEnumerable<Portfolio> Get(string ownerEmail);
    public class CreatePortfolioModel
    {
        public string Name { get; set; }
    }
}