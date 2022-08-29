using Api.Infrastructure;
using Api.Services;
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
}