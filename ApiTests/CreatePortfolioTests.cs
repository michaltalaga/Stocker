using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTests;

public class CreatePortfolioTests
{
    CreatePortfolio createPortfolio;
    Container container;
    public CreatePortfolioTests()
    {
        container = Substitute.For<Container>();
        var database = Substitute.For<Database>();
        database.GetContainer(Arg.Any<string>()).Returns(container);
        createPortfolio = new CreatePortfolio(database);
    }
    [Fact]
    public async Task AddsNewPortfolioToDatabase()
    {
        var portfolio = PortfolioStub.Create();
        await createPortfolio.Run(portfolio);
        await container.Received().CreateItemAsync(portfolio);
    }
    [Fact]
    public async Task ReturnsCreatedCodeForNewPortfolios()
    {
        var portfolio = PortfolioStub.Create();
        var statusCodeResult = await createPortfolio.Run(portfolio) as StatusCodeResult;
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult!.StatusCode);
    }
}