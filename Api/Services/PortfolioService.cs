namespace Api.Services;

public class PortfolioService : IPortfolioService
{
    private IUserContext userContext;
    private IRepository repository;

    public PortfolioService(IUserContext userContext, IRepository repository)
    {
        this.userContext = userContext;
        this.repository = repository;
    }

    public async Task Create(Portfolio portfolio)
    {
        EnsureOwnerEmail(portfolio);
        await repository.Add(portfolio);
    }

    private void EnsureOwnerEmail(Portfolio portfolio)
    {
        var currentUserEmail = userContext.GetEmail();
        portfolio.OwnerEmail ??= currentUserEmail;
        if (!portfolio.OwnerEmail.Equals(currentUserEmail, StringComparison.InvariantCultureIgnoreCase)) throw new InvalidOperationException();
    }
}