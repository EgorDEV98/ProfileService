using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProfileService.Data.EntityConfigurations;

public class VersionConfiguration : IEntityTypeConfiguration<Entities.Version>
{
    public void Configure(EntityTypeBuilder<Entities.Version> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VersionNumber).IsRequired();
        builder.Property(x => x.IsInstalled).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTimeOffset.Now);
    }
}