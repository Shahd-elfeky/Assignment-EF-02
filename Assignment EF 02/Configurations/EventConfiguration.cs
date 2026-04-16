using Assignment_EF_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment_EF_02.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(1000);

        // Self referencing for sessions
        builder.HasOne(e => e.ParentEvent)
               .WithMany(e => e.Sessions)
               .HasForeignKey(e => e.ParentEventId)
               .OnDelete(DeleteBehavior.Restrict);

        // Organizer
        builder.HasOne(e => e.Organizer)
               .WithMany()
               .HasForeignKey(e => e.OrganizerId)
               .OnDelete(DeleteBehavior.Restrict);

        // Shadow Properties for tracking dates
        builder.Property<DateTime>("CreatedDate");
        builder.Property<DateTime>("LastModifiedDate");
    }
}
