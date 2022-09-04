using Api;
using Api.Infrastructure;
using Api.Services;
using Api.UglyStuff;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("StockerCosmosDB") ?? Environment.GetEnvironmentVariable("ConnectionStrings:StockerCosmosDB");
        var cosmosClientBuilder = new CosmosClientBuilder(connectionString);
        cosmosClientBuilder.WithCustomSerializer(new CosmosDbSerializer(new System.Text.Json.JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        }));
        var cosmosClient = cosmosClientBuilder.Build();

        builder.Services.AddTransient(services => cosmosClient.GetDatabase("Stocker"));
        builder.Services.AddTransient<IRepository, CosmosDBRepository>();
        builder.Services.AddTransient<IUserContext, FromConfigUserContext>();
        builder.Services.AddTransient<IPortfolioService, PortfolioService>();
    }
}