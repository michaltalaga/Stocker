using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure;

public class FromConfigUserContext : IUserContext
{
    private readonly IConfiguration configuration;

    public FromConfigUserContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string GetEmail()
    {
        return configuration["CurrentUserEmail"];
    }
}