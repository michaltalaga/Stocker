using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;

namespace Api
{
    public class CreatePortfolio
    {
        private readonly Database database;

        public CreatePortfolio(Database database)
        {
            this.database = database;
        }
        [FunctionName("CreatePortfolio")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] Models.Portfolio portfolio)
        {
            
            var container = database.GetContainer("Portfolios");

            await container.CreateItemAsync(portfolio);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
    }
}
