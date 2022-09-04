using static Api.Services.IPortfolioService;

namespace ApiTests;

internal static class PortfolioStub
{
    public const string OwnerEmail = "owner@email.com";
    public const string PortfolioName = "Portfolio 1";
    public static CreatePortfolioModel CreateNewCreatePortfolioModel() => new CreatePortfolioModel { Name = PortfolioName };

    public static AddTransactionModel CreateNewAddTransactionModel() => new AddTransactionModel
    {
        Symbol = "SYMBOL",
        //Date = new DateTimeOffset(2001, 1, 1, 1, 1, 1, TimeSpan.Zero),
        Date = "2001-01-01",
        PricePerShare = 10,
        Quantity = 10,
        //Type = TransactionType.Buy
        BuySell = TransactionType.Buy.ToString()
    };

    public static Portfolio[] CreateNewPortfolioCollection() => new Portfolio[]
    {
        new Portfolio { Name = PortfolioName, OwnerEmail = OwnerEmail },
        new Portfolio { Name = "p1", OwnerEmail = "xxx" }
    };
}