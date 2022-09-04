using Api.Infrastructure;
using Api.Models;
using Api.Services;
using NSubstitute;
using static Api.Services.IPortfolioService;

namespace ApiTests.Services;

public class PortfolioServiceTests
{
    PortfolioService portfolioService;
    IRepository repository;
    IUserContext userContext;
    public PortfolioServiceTests()
    {
        repository = Substitute.For<IRepository>();
        
        repository.Query<Portfolio>().Returns(PortfolioStub.CreateNewPortfolioCollection().AsQueryable());
        userContext = Substitute.For<IUserContext>();
        userContext.GetEmail().Returns(PortfolioStub.OwnerEmail);
        portfolioService = new PortfolioService(repository);
    }
    [Fact]
    public async Task CreateAddsNewPortfolioToRepositoryWithNameAndOwnerEmail()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        await portfolioService.Create(PortfolioStub.OwnerEmail, createPortfolioModel);
        await repository.Received().Add(Arg.Is<Portfolio>(p => p.OwnerEmail == PortfolioStub.OwnerEmail && p.Name == createPortfolioModel.Name));
    }
    [Fact]
    public async Task CreateThrowsIfNoOwnerEmailProvided()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        var task = portfolioService.Create(null, createPortfolioModel);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);
    }
    [Fact]
    public async Task CreateThrowsIfNoModelProvided()
    {
        var task = portfolioService.Create(PortfolioStub.OwnerEmail, null);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);
    }

    [Fact]
    public async Task CreateThrowsIfNameMissing()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        createPortfolioModel.Name = null;
        var task = portfolioService.Create(PortfolioStub.OwnerEmail, createPortfolioModel);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);
    }
    [Fact]
    public void GetUsesOwnerEmail()
    {
        var result = portfolioService.Get(PortfolioStub.OwnerEmail).ToArray();
        Assert.True(result.Length == 1);
        Assert.True(result[0].OwnerEmail == PortfolioStub.OwnerEmail);
    }

    [Fact]
    public async Task AddTransactionGetsExistingPortfolio()
    {
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        await portfolioService.AddTransaction(PortfolioStub.OwnerEmail, "p1", transaction);
        repository.Received().Query<Portfolio>();
    }

    [Fact]
    public async Task AddTransactionSavesExisingPortfolioWithNewTransaction()
    {
        var portfolio = PortfolioStub.CreateNewPortfolioCollection().First();
        repository.Query<Portfolio>().Returns((new[] { portfolio }).AsQueryable());
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        await portfolioService.AddTransaction(portfolio.OwnerEmail, portfolio.Name, transaction);
        await repository.Received().Update(Arg.Is<Portfolio>(p => p.Transactions.Count() == 1));
    }

    [Fact]
    public async Task AddTransactionToNonExistingPortfolioThrows()
    {
        var task = portfolioService.AddTransaction(PortfolioStub.OwnerEmail, "x", PortfolioStub.CreateNewAddTransactionModel());
        await Assert.ThrowsAnyAsync<Exception>(() => task);
    }

    [Fact]
    public async Task AddTransactionMapsProperties()
    {
        var portfolio = PortfolioStub.CreateNewPortfolioCollection().First();
        repository.Query<Portfolio>().Returns((new[] { portfolio }).AsQueryable());
        var addTransactionModel = PortfolioStub.CreateNewAddTransactionModel();
        await portfolioService.AddTransaction(portfolio.OwnerEmail, portfolio.Name, addTransactionModel);
        var verifyMapping = (Portfolio p) =>
        {
            var transaction = p.Transactions.First();
            return addTransactionModel.Symbol == transaction.Symbol
                //&& addTransactionModel.Date == transaction.Date
                //&& addTransactionModel.Type == transaction.Type
                && addTransactionModel.BuySell.ToString() == transaction.Type.ToString()
                && addTransactionModel.Date == transaction.Date.ToString("yyyy-MM-dd")
                && addTransactionModel.Quantity == transaction.Quantity
                && addTransactionModel.PricePerShare == transaction.PricePerShare;
        };
        await repository.Received().Update(Arg.Is<Portfolio>(p => verifyMapping(p)));
    }
    [Fact]
    public async Task AddTransactionValidatesInputs()
    {
        var portfolio = PortfolioStub.CreateNewPortfolioCollection().First();
        repository.Query<Portfolio>().Returns((new[] { portfolio }).AsQueryable());
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        var task = portfolioService.AddTransaction(null, portfolio.Name, transaction);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);

        task = portfolioService.AddTransaction(portfolio.OwnerEmail, null, transaction);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);

        task = portfolioService.AddTransaction(portfolio.OwnerEmail, portfolio.Name, null);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);

        transaction.Symbol = null;
        task = portfolioService.AddTransaction(portfolio.OwnerEmail, portfolio.Name, transaction);
        await Assert.ThrowsAsync<ArgumentNullException>(() => task);
    }
    [Fact]
    public async Task AddTransactionNewTransactionIncrementSequenceNumbers()
    {
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        await portfolioService.AddTransaction(PortfolioStub.OwnerEmail, PortfolioStub.PortfolioName, transaction);
        await repository.Received().Update(Arg.Is<Portfolio>(p => p.Transactions.Last().SequenceNumber == 0));
        await portfolioService.AddTransaction(PortfolioStub.OwnerEmail, PortfolioStub.PortfolioName, transaction);
        await repository.Received().Update(Arg.Is<Portfolio>(p => p.Transactions.Last().SequenceNumber == 1));
    }
}