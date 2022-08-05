using Api.Endpoints;
using Api.Infrastructure;
using Api.Services;

namespace ApiTests.Services;

public class PortfolioServiceTests
{
    PortfolioService createPortfolio;
    IRepository repository;
    IUserContext userContext;
    public PortfolioServiceTests()
    {
        repository = Substitute.For<IRepository>();
        userContext = Substitute.For<IUserContext>();
        userContext.GetEmail().Returns(PortfolioStub.OwnerEmail);
        createPortfolio = new PortfolioService(userContext, repository);
    }
    [Fact]
    public async Task CreateAddsNewPortfolioToDatabase()
    {
        var portfolio = PortfolioStub.Create();
        await createPortfolio.Create(portfolio);
        await repository.Received().Add(portfolio);
    }
    [Fact]
    public async Task CreatingPortfolioWithNullOwnerEmailShouldUseCurrentUserEmail()
    {
        var portfolio = PortfolioStub.Create();
        portfolio.OwnerEmail = null;
        await createPortfolio.Create(portfolio);
        await repository.Received().Add(Arg.Is<Portfolio>(p => p.OwnerEmail == userContext.GetEmail()));
    }
    [Fact]
    public async Task CreatingPortfolioWithCurrentOwnerEmailShouldUseIt()
    {
        var portfolio = PortfolioStub.Create();
        portfolio.OwnerEmail = PortfolioStub.OwnerEmail;
        await createPortfolio.Create(portfolio);
        await repository.Received().Add(Arg.Is<Portfolio>(p => p.OwnerEmail == PortfolioStub.OwnerEmail));
    }
    [Fact]
    public async Task CreatingPortfolioWithOwnerEmailDifferentThanCurrentUserEmailShouldThrow()
    {
        var portfolio = PortfolioStub.Create();
        portfolio.OwnerEmail = "not" + userContext.GetEmail();
        var task = createPortfolio.Create(portfolio);
        await Assert.ThrowsAsync<InvalidOperationException>(() => task);
    }
}