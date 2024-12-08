using Microsoft.EntityFrameworkCore;
using ProfileService.Data;

namespace ProfileService.Application.Services;

public class SystemService
{
    private readonly ProfileServiceDbContext _context;

    public SystemService(ProfileServiceDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Получить версию приложения
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<int> GetVersionAsync(CancellationToken ct)
    {
        return await _context.Versions.MaxAsync(v => (int?)v.VersionNumber, cancellationToken: ct) ?? 0;
    }
}