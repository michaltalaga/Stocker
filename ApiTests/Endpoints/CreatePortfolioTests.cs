using Api.Endpoints;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTests.Endpoints;

public class CreatePortfolioTests
{
    CreatePortfolio createPortfolio;
    IPortfolioService portfolioService;
    public CreatePortfolioTests()
    {
        portfolioService = Substitute.For<IPortfolioService>();
        createPortfolio = new CreatePortfolio(portfolioService);
    }
    [Fact]
    public async Task CallsCreateOnService()
    {
        var portfolio = PortfolioStub.Create();
        await createPortfolio.Run(portfolio);
        await portfolioService.Received().Create(portfolio);
    }
    [Fact]
    public async Task ReturnsStatusCodeCreatedOnSucceeds()
    {
        var portfolio = PortfolioStub.Create();
        var statusCodeResult = await createPortfolio.Run(portfolio) as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult!.StatusCode);
    }
}