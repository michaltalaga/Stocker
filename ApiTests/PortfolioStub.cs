using static Api.Services.IPortfolioService;

namespace ApiTests;

internal static class PortfolioStub
{
    public const string OwnerEmail = "owner@email.com";
    public static CreatePortfolioModel CreateNewCreatePortfolioModel() => new CreatePortfolioModel { Name = "Portfolio 1" };

    public static AddTransactionModel CreateNewAddTransactionModel() => new AddTransactionModel
    {
        Symbol = "SYMBOL",
        //Date = new DateTimeOffset(2001, 1, 1, 1, 1, 1, TimeSpan.Zero),
        DateString = "2001-01-01",
        PricePerShare = 10,
        Quantity = 10,
        //Type = TransactionType.Buy
        BuySell = TransactionType.Buy.ToString()
    };

    public static Portfolio[] CreateNewPortfolioCollection() => new Portfolio[]
    {
        new Portfolio { Name = "p1", OwnerEmail = OwnerEmail },
        new Portfolio { Name = "p1", OwnerEmail = "xxx" }
    };
}