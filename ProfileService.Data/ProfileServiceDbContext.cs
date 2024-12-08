using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Entities;
using ProfileService.Data.EntityConfigurations;
using Version = ProfileService.Data.Entities.Version;

namespace ProfileService.Data;

public class ProfileServiceDbContext : DbContext
{
    /// <summary>
    /// Профили
    /// </summary>
    public DbSet<Profile> Profiles { get; set; }
    
    /// <summary>
    /// Версии
    /// </summary>
    public DbSet<Version> Versions { get; set; }
    
    public ProfileServiceDbContext(DbContextOptions<ProfileServiceDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new VersionConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}