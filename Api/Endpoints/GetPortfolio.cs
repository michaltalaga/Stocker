using Api.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;

namespace Api.Endpoints;

public class GetPortfolio
{
    private readonly IPortfolioService portfolioService;
    private readonly IUserContext userContext;

    public GetPortfolio(IPortfolioService portfolioService, IUserContext userContext)
    {
        this.portfolioService = portfolioService;
        this.userContext = userContext;
    }
    [FunctionName("GetPortfolios")]
    public IEnumerable<Portfolio> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "portfolios")] HttpRequestMessage req)
    {
        return portfolioService.Get(userContext.GetEmail());
    }

    [FunctionName("GetPortfolio")]
    public Portfolio Run1(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "portfolios/{name}")] HttpRequestMessage req, string name)
    {
        return portfolioService.Get(userContext.GetEmail(), name);
    }
}