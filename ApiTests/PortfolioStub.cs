namespace ApiTests;

internal static class PortfolioStub
{
    public static Portfolio Create() => new Portfolio
    {
        Name = "p1",
        OwnerEmail = "owner@email.com",
    };
}