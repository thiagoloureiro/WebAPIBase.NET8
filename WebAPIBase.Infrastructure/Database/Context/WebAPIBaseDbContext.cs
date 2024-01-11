using Microsoft.EntityFrameworkCore;
using WebAPIBase.Domain.Entities;
using WebAPIBase.Infrastructure.Database.Context.Configurations;

namespace WebAPIBase.Infrastructure.Database.Context;

public class WebAPIBaseDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public WebAPIBaseDbContext(DbContextOptions<WebAPIBaseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CountryTypeConfiguration().Configure(modelBuilder.Entity<Country>());
    }
}