using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;


/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().HasKey(f => f.id);
        builder.Entity<User>().Property(f => f.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(f => f.username).IsRequired();
        builder.Entity<User>().Property(f => f.first_name).IsRequired();
        builder.Entity<User>().Property(f => f.last_name).IsRequired();
        builder.Entity<User>().Property(f => f.email).IsRequired();
        builder.Entity<User>().Property(f => f.phone).IsRequired();
        builder.Entity<User>().Property(f => f.register_date).IsRequired();
        builder.Entity<User>().Property(f => f.company).IsRequired();
        builder.Entity<User>().Property(f => f.password).IsRequired();
        
        builder.UseSnakeCaseNamingConvention();
    }
}