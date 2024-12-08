using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProfileService.Data;

namespace ProfileService.Application.Common;

public class PostgresHealthCheck : IHealthCheck
{
    private readonly IServiceProvider _serviceProvider;

    public PostgresHealthCheck(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProfileServiceDbContext>();

        try
        {
            await dbContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
            return HealthCheckResult.Healthy("ready");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return HealthCheckResult.Unhealthy("not ready");
        }
    }
}