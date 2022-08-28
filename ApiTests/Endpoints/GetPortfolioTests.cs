using Api.Endpoints;
using Api.Infrastructure;
using Api.Services;

namespace ApiTests.Endpoints;

public class GetPortfolioTests
{
    GetPortfolio getPortfolio;
    IPortfolioService portfolioService;
    IUserContext userContext;
    public GetPortfolioTests()
    {
        portfolioService = Substitute.For<IPortfolioService>();
        userContext = Substitute.For<IUserContext>();
        userContext.GetEmail().Returns(PortfolioStub.OwnerEmail);
        getPortfolio = new GetPortfolio(portfolioService, userContext);
    }
    [Fact]
    public void GetPortfoliosUsesOwnerEmail()
    {
        getPortfolio.Run(new HttpRequestMessage());
        portfolioService.Received().Get(userContext.GetEmail());
    }

    [Fact]
    public void GetPortfolioUsesOwnerEmailAndName()
    {
        var name = "name1";
        getPortfolio.Run1(new HttpRequestMessage(), name);
        portfolioService.Received().Get(userContext.GetEmail(), name);
    }
}