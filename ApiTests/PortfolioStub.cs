using Api.Services;
using static Api.Services.IPortfolioService;

namespace ApiTests;

internal static class PortfolioStub
{
    public const string OwnerEmail = "owner@email.com";
    public static CreatePortfolioModel CreateNewCreatePortfolioModel() => new CreatePortfolioModel { Name = "Portfolio 1" };

    public static AddTransactionModel CreateNewAddTransactionModel() => new AddTransactionModel();

    public static Portfolio[] CreateNewPortfolioCollection() => new Portfolio[]
    {
        new Portfolio { Name = "p1", OwnerEmail = OwnerEmail },
        new Portfolio { Name = "p1", OwnerEmail = "xxx" }
    };
}