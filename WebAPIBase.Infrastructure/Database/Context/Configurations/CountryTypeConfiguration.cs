using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPIBase.Domain.Entities;

namespace WebAPIBase.Infrastructure.Database.Context.Configurations;

public class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .Property(x => x.Id)
            .UseIdentityColumn();

        builder.HasKey(x => x.Id).IsClustered();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.CountryCode)
            .IsRequired();

        builder.HasIndex(x => x.CountryCode)
            .IsUnique();
    }
}