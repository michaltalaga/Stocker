namespace Api.Services;

public interface IPortfolioService
{
    Task Create(Portfolio portfolio);
}