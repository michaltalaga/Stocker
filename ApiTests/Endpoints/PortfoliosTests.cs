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

    [Fact]
    public async Task DeletePortfolioCallsDeleteOnService()
    {
        await portfolios.DeletePortfolio(new HttpRequestMessage(), PortfolioStub.PortfolioName);
        await portfolioService.Received().Delete(userContext.GetEmail(), PortfolioStub.PortfolioName);
    }

    [Fact]
    public async Task AddTransactionCallsAddOnServiceWithOwnerEmailPortfolioNameAndTransaction()
    {
        var portfolioName = "p1";
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        await portfolios.AddTransaction(transaction, portfolioName);
        await portfolioService.Received().AddTransaction(userContext.GetEmail(), portfolioName, transaction);
    }

    [Fact]
    public async Task AddTransactionReturnsStatusCodeCreatedOnSuccess()
    {
        var portfolioName = "p1";
        var transaction = PortfolioStub.CreateNewAddTransactionModel();
        var statusCodeResult = await portfolios.AddTransaction(transaction, portfolioName) as StatusCodeResult;
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult!.StatusCode);
    }

    [Fact]
    public async Task DeleteTransactionCallsDeleteOnServiceWithOwnerEmailPortfolioNameAndSequence()
    {
        var portfolioName = "p1";
        var sequenceNumber = 1;
        await portfolios.DeleteTransaction(new HttpRequestMessage(), portfolioName, sequenceNumber);
        await portfolioService.Received().DeleteTransaction(userContext.GetEmail(), portfolioName, sequenceNumber);
    }
    [Fact]
    public async Task DeleteTransactionReturnsStatusCodeOkOnSuccess()
    {
        var portfolioName = "p1";
        var statusCodeResult = await portfolios.DeleteTransaction(new HttpRequestMessage(), portfolioName, 1) as StatusCodeResult;
        Assert.Equal(StatusCodes.Status200OK, statusCodeResult!.StatusCode);
    }
}