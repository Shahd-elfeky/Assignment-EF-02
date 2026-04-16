using Assignment_EF_02.Configurations;
using Assignment_EF_02.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_EF_02.Data;

public class EventHubDbContext : DbContext
{
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<OrganizerProfile> Profiles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<EventAttendee> EventAttendees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applying Event Configuration
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        // Fluent API for Attendee
        modelBuilder.Entity<Attendee>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.FullName).IsRequired().HasMaxLength(150);
            entity.Property(a => a.EmailAddress).IsRequired().HasMaxLength(150);
            entity.Property(a => a.Street).HasMaxLength(100);
            entity.Property(a => a.City).HasMaxLength(100);
            entity.Property(a => a.Country).HasMaxLength(100);
            entity.Property(a => a.PostalCode).HasMaxLength(20);
        });

        // Fluent API for Badge
        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.CredentialNumber).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Tier).IsRequired().HasMaxLength(20);

            entity.HasOne(b => b.Attendee)
                  .WithOne(a => a.Badge)
                  .HasForeignKey<Badge>(b => b.AttendeeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Fluent API for EventAttendee
        modelBuilder.Entity<EventAttendee>(entity =>
        {
            entity.HasKey(ea => new { ea.EventId, ea.AttendeeId });

            entity.HasOne(ea => ea.Event)
                  .WithMany(e => e.EventAttendees)
                  .HasForeignKey(ea => ea.EventId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ea => ea.Attendee)
                  .WithMany(a => a.EventAttendees)
                  .HasForeignKey(ea => ea.AttendeeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(ea => ea.NotesToOrganizer).HasMaxLength(500);
        });

        // Profile mapping (to enforce the principal/dependent relationship implicitly/correctly if needed)
        modelBuilder.Entity<Organizer>()
            .HasOne(o => o.Profile)
            .WithOne(p => p.Organizer)
            .HasForeignKey<OrganizerProfile>(p => p.OrganizerId);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<Event>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Property("LastModifiedDate").CurrentValue = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}
