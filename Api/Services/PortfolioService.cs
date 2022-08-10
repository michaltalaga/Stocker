using static Api.Services.IPortfolioService;

namespace Api.Services;

public class PortfolioService : IPortfolioService
{
    private IRepository repository;

    public PortfolioService(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(string ownerEmail, CreatePortfolioModel portfolio)
    {
        ArgumentNullException.ThrowIfNull(ownerEmail);
        ArgumentNullException.ThrowIfNull(portfolio);
        await repository.Add(new Portfolio
        {
            OwnerEmail = ownerEmail,
            Name = portfolio.Name,
        });
    }
}