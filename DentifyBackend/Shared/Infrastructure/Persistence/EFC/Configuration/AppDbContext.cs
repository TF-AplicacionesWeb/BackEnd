using DentifyBackend.Odontology.Domain.Model.Aggregates;
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


        builder.Entity<Payments>().HasKey(p => p.id);
        builder.Entity<Payments>().Property(p => p.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payments>().Property(p => p.amount).IsRequired();
        builder.Entity<Payments>().Property(p => p.payment_date).IsRequired();
        builder.Entity<Payments>().HasOne<User>().WithMany().HasForeignKey(p => p.user_id);

        builder.Entity<Inventory>().HasKey(i => i.id);
        builder.Entity<Inventory>().Property(i => i.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Inventory>().Property(i => i.material_name).IsRequired();
        builder.Entity<Inventory>().Property(i => i.quantity).IsRequired();
        builder.Entity<Inventory>().Property(i => i.unit_price).IsRequired();
        builder.Entity<Inventory>().Property(i => i.last_updated).IsRequired();
        builder.Entity<Inventory>().HasOne<User>().WithMany().HasForeignKey(i => i.user_id)

            .OnDelete(DeleteBehavior.Cascade);
        
        

        builder.Entity<ClinicalRecord>().Property(c => c.id).IsRequired();
        builder.Entity<ClinicalRecord>().Property(c => c.medical_history).IsRequired();
        builder.Entity<ClinicalRecord>().Property(c => c.record_date).IsRequired();
        builder.Entity<ClinicalRecord>().Property(c => c.diagnosis).IsRequired();
        builder.Entity<ClinicalRecord>().Property(c => c.treatment).IsRequired();
        builder.Entity<ClinicalRecord>()
            .HasOne<User>()               
            .WithMany()         
            .HasForeignKey(c => c.user_id)     
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.Entity<Patient>().HasKey(p => p.id);
        builder.Entity<Patient>().Property(p => p.id).IsRequired();
        builder.Entity<Patient>().Property(p => p.clinical_record_id).IsRequired();
        builder.Entity<Patient>()
            .HasOne<ClinicalRecord>()               
            .WithMany()         
            .HasForeignKey(s => s.clinical_record_id)     
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Patient>().Property(p => p.first_name).IsRequired();
        builder.Entity<Patient>().Property(p => p.last_name).IsRequired();
        builder.Entity<Patient>().Property(p => p.email).IsRequired();
        builder.Entity<Patient>().Property(p => p.age).IsRequired();
        builder.Entity<Patient>().Property(p => p.medical_history).IsRequired();
        builder.Entity<Patient>().Property(p => p.birth_date).IsRequired();
        
        builder.Entity<Patient>().Property(p => p.appointment_id).IsRequired(false);
        builder.Entity<Patient>()
            .HasOne<Appointment>()
            .WithMany()
            .HasForeignKey(p => p.appointment_id)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        
        builder.Entity<Patient>().Property(p => p.user_id).IsRequired();
        builder.Entity<Patient>()
            .HasOne<User>()               
            .WithMany()         
            .HasForeignKey(s => s.user_id)     
            .OnDelete(DeleteBehavior.Cascade);
        

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

        builder.Entity<Appointment>().HasKey(s => s.id);
        builder.Entity<Appointment>().Property(s => s.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Appointment>()
            .HasOne<Dentist>()
            .WithMany()
            .HasForeignKey(a => a.dentist_dni)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Appointment>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.user_id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>().Property(s => s.appointment_date).IsRequired();
        builder.Entity<Appointment>().Property(s => s.reason).IsRequired();
        builder.Entity<Appointment>().Property(s => s.completed).IsRequired();
        builder.Entity<Appointment>().Property(s => s.reminder_sent).IsRequired();
        builder.Entity<Appointment>().Property(s => s.duration_minutes).IsRequired();
        builder.Entity<Appointment>().HasOne<Payments>()
            .WithOne()
            .HasForeignKey<Appointment>(p => p.payment_id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Appointment>().Property(s => s.payment_status).IsRequired();
        

        builder.UseSnakeCaseNamingConvention();
    }
}