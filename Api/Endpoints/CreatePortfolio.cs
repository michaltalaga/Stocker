using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services;
using static Api.Services.IPortfolioService;

namespace Api.Endpoints
{
    public class CreatePortfolio
    {
        private readonly IPortfolioService portfolioService;
        private readonly IUserContext userContext;

        public CreatePortfolio(IPortfolioService portfolioService, IUserContext userContext)
        {
            this.portfolioService = portfolioService;
            this.userContext = userContext;
        }
        [FunctionName("CreatePortfolio")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] CreatePortfolioModel portfolio)
        {
            await portfolioService.Create(userContext.GetEmail(), portfolio);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
    }
}