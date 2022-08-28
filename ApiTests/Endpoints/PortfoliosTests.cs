using Api.Endpoints;
using Api.Infrastructure;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTests.Endpoints;

public class PortfoliosTests
{
    Portfolios portfolios;
    IPortfolioService portfolioService;
    IUserContext userContext;
    public PortfoliosTests()
    {
        portfolioService = Substitute.For<IPortfolioService>();
        userContext = Substitute.For<IUserContext>();
        userContext.GetEmail().Returns(PortfolioStub.OwnerEmail);
        portfolios = new Portfolios(portfolioService, userContext);
    }
    [Fact]
    public void GetPortfoliosUsesOwnerEmail()
    {
        portfolios.GetPortfolios(new HttpRequestMessage());
        portfolioService.Received().Get(userContext.GetEmail());
    }

    [Fact]
    public void GetPortfolioUsesOwnerEmailAndName()
    {
        var name = "name1";
        portfolios.GetPortfolio(new HttpRequestMessage(), name);
        portfolioService.Received().Get(userContext.GetEmail(), name);
    }
    [Fact]
    public async Task CreatePortfolioCallsCreateOnServiceWithOwnerEmailAndModel()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        await portfolios.CreatePortfolio(createPortfolioModel);
        await portfolioService.Received().Create(userContext.GetEmail(), createPortfolioModel);
    }
    [Fact]
    public async Task CreatePortfolioReturnsStatusCodeCreatedOnSuccess()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        var statusCodeResult = await portfolios.CreatePortfolio(createPortfolioModel) as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult!.StatusCode);
    }
}