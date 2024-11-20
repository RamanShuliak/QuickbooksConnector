using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickbooksConnector.Services.Configurations;
using QuickbooksConnector.Services.Services;

namespace QuickbooksConnector.Services.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<QuickBooksConfig>(configuration.GetSection("QuickBooksConfig"));
        services.AddScoped<IQuickBooksClientService, QuickBooksClientService>();
        services.AddScoped<IXmlParsingService, XmlParsingService>();
        services.AddScoped<ICompanyService, CompanyService>();

        return services;
    }
}
