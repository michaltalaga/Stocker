using Api.Services;
using static Api.Services.IPortfolioService;

namespace ApiTests;

internal static class PortfolioStub
{
    public const string OwnerEmail = "owner@email.com";
    public static CreatePortfolioModel CreateNewCreatePortfolioModel() => new CreatePortfolioModel { Name = "Portfolio 1" };
}