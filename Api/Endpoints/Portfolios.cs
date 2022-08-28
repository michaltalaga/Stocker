using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services;
using static Api.Services.IPortfolioService;
using System.Net.Http;

namespace Api.Endpoints
{
    public class Portfolios
    {
        private readonly IPortfolioService portfolioService;
        private readonly IUserContext userContext;

        public Portfolios(IPortfolioService portfolioService, IUserContext userContext)
        {
            this.portfolioService = portfolioService;
            this.userContext = userContext;
        }

        [FunctionName(nameof(CreatePortfolio))]
        public async Task<IActionResult> CreatePortfolio(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "portfolios")] CreatePortfolioModel portfolio)
        {
            await portfolioService.Create(userContext.GetEmail(), portfolio);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
        [FunctionName(nameof(GetPortfolios))]
        public IEnumerable<Portfolio> GetPortfolios(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "portfolios")] HttpRequestMessage req)
        {
            return portfolioService.Get(userContext.GetEmail());
        }

        [FunctionName(nameof(GetPortfolio))]
        public Portfolio GetPortfolio(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "portfolios/{name}")] HttpRequestMessage req, string name)
        {
            return portfolioService.Get(userContext.GetEmail(), name);
        }
    }
}