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
}