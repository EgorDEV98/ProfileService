using System.Reflection;
using ProfileService.Application.Common;
using ProfileService.WebApi.Common;

namespace ProfileService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            string[] envsLocal =  ["Local"];
            if (envsLocal.Contains(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) == false)
            {
                options.DocumentFilter<PathPrefixInsertDocumentFilter>("/Api/Profile");
            }
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        } );
        
        return services;
    }
}