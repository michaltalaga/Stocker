using Api;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("StockerCosmosDB") ?? Environment.GetEnvironmentVariable("ConnectionStrings:StockerCosmosDB");
        var cosmosClientBuilder = new CosmosClientBuilder(connectionString);
        var cosmosClient = cosmosClientBuilder.Build();

        builder.Services.AddTransient(services => cosmosClient.GetDatabase("Stocker"));
    }
}