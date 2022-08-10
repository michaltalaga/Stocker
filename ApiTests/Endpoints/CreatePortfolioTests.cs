using Api.Endpoints;
using Api.Infrastructure;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTests.Endpoints;

public class CreatePortfolioTests
{
    CreatePortfolio createPortfolio;
    IPortfolioService portfolioService;
    IUserContext userContext;
    public CreatePortfolioTests()
    {
        portfolioService = Substitute.For<IPortfolioService>();
        userContext = Substitute.For<IUserContext>();
        userContext.GetEmail().Returns(PortfolioStub.OwnerEmail);
        createPortfolio = new CreatePortfolio(portfolioService, userContext);
    }
    [Fact]
    public async Task CallsCreateOnServiceWithOwnerEmailAndModel()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        await createPortfolio.Run(createPortfolioModel);
        await portfolioService.Received().Create(userContext.GetEmail(), createPortfolioModel);
    }
    [Fact]
    public async Task ReturnsStatusCodeCreatedOnSucceeds()
    {
        var createPortfolioModel = PortfolioStub.CreateNewCreatePortfolioModel();
        var statusCodeResult = await createPortfolio.Run(createPortfolioModel) as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult!.StatusCode);
    }
}