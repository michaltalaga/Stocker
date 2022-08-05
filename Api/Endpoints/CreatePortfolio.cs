using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services;

namespace Api.Endpoints
{
    public class CreatePortfolio
    {
        private readonly IPortfolioService portfolioService;

        public CreatePortfolio(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }
        [FunctionName("CreatePortfolio")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] Portfolio portfolio)
        {
            await portfolioService.Create(portfolio);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
    }
}