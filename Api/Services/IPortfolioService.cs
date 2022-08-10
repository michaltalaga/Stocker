namespace Api.Services;

public interface IPortfolioService
{
    Task Create(string ownerEmail, CreatePortfolioModel portfolio);
    public class CreatePortfolioModel
    {
        public string Name { get; set; }
    }
}