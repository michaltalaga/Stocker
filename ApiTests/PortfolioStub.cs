namespace ApiTests;

internal static class PortfolioStub
{
    public const string OwnerEmail = "owner@email.com";
    public static Portfolio Create() => new Portfolio
    {
        Name = "p1",
        OwnerEmail = OwnerEmail,
    };
}