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
        var portfolios = new Portfolio[]
        {
            new Portfolio{ OwnerEmail = PortfolioStub.OwnerEmail},
            new Portfolio { OwnerEmail = "xxx" }
        }.AsQueryable();
        repository.Query<Portfolio>().Returns(portfolios);
        var result = portfolioService.Get(PortfolioStub.OwnerEmail).ToArray();
        Assert.True(result.Length == 1);
        Assert.True(result[0].OwnerEmail == PortfolioStub.OwnerEmail);
    }
}