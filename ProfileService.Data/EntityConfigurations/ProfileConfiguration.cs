using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Data.Entities;

namespace ProfileService.Data.EntityConfigurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.TinkoffApiKey).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTimeOffset.Now);
        builder.Property(x => x.LastUpdate).IsRequired().HasDefaultValue(DateTimeOffset.Now);
    }
}