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
        ArgumentNullException.ThrowIfNull(portfolio.Name);
        await repository.Add(new Portfolio
        {
            OwnerEmail = ownerEmail,
            Name = portfolio.Name,
        });
    }
    public IEnumerable<Portfolio> Get(string ownerEmail)
    {
        return repository.Query<Portfolio>().Where(p => p.OwnerEmail == ownerEmail);
    }

    public Portfolio Get(string ownerEmail, string name)
    {
        return repository.Query<Portfolio>().Where(p => p.OwnerEmail == ownerEmail && p.Name == name).AsEnumerable().Single();
    }

    public async Task AddTransaction(string ownerEmail, string portfolioName, AddTransactionModel transaction)
    {
        ArgumentNullException.ThrowIfNull(ownerEmail);
        ArgumentNullException.ThrowIfNull(portfolioName);
        ArgumentNullException.ThrowIfNull(transaction);
        ArgumentNullException.ThrowIfNull(transaction.Symbol);
        var portfolio = Get(ownerEmail, portfolioName);
        portfolio.Transactions ??= new List<Transaction>();
        portfolio.Transactions.Add(new Transaction
        {
            Symbol = transaction.Symbol,
            //Date = transaction.Date,
            Date = DateTimeOffset.Parse(transaction.DateString),
            Quantity = transaction.Quantity,
            PricePerShare = transaction.PricePerShare,
            //Type = transaction.Type,
            Type = Enum.Parse<TransactionType>(transaction.BuySell)
        });
        await repository.Update(portfolio);
    }
}