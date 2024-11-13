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
        builder.Entity<User>().Property(f => f.trial).IsRequired();
        
        
        builder.Entity<Dentist>().HasKey(d => d.id);
        builder.Entity<Dentist>().Property(d => d.id).IsRequired();
        builder.Entity<Dentist>().Property(d => d.first_name).IsRequired();
        builder.Entity<Dentist>().Property(d => d.last_name).IsRequired();
        builder.Entity<Dentist>().Property(d => d.specialty).IsRequired();
        builder.Entity<Dentist>().Property(d => d.experience).IsRequired();
        builder.Entity<Dentist>().Property(d => d.phone).IsRequired();
        builder.Entity<Dentist>().Property(d => d.email).IsRequired();
        builder.Entity<Dentist>().Property(d => d.total_appointments).IsRequired();
        builder.Entity<Dentist>()
            .HasOne<User>()               
            .WithMany()         
            .HasForeignKey(d => d.user_id)     
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.Entity<ScheduleDentist>().HasKey(s => s.id);
        builder.Entity<ScheduleDentist>().Property(s => s.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ScheduleDentist>().Property(s => s.weekday).IsRequired();
        builder.Entity<ScheduleDentist>().Property(s => s.start_time).IsRequired();
        builder.Entity<ScheduleDentist>().Property(s => s.end_time).IsRequired();
        builder.Entity<ScheduleDentist>().Property(s => s.date).IsRequired();
        builder.Entity<ScheduleDentist>()
            .HasOne<User>()               
            .WithMany()         
            .HasForeignKey(d => d.user_id)     
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ScheduleDentist>()
            .HasOne<Dentist>()
            .WithMany(d => d.schedules)
            .HasForeignKey(s => s.dentist_id);
        
        
        builder.Entity<SupportMessage>().HasKey(s => s.id);
        builder.Entity<SupportMessage>().Property(s => s.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SupportMessage>().Property(s => s.name).IsRequired();
        builder.Entity<SupportMessage>().Property(s => s.email).IsRequired();
        builder.Entity<SupportMessage>().Property(s => s.description).IsRequired();
        builder.Entity<SupportMessage>()
            .HasOne<User>()               
            .WithMany()         
            .HasForeignKey(s => s.user_id)     
            .OnDelete(DeleteBehavior.Cascade);

        builder.UseSnakeCaseNamingConvention();
    }
}