namespace Assignment_EF_02.Models;

public class EventAttendee
{
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; } = null!;

    public string? NotesToOrganizer { get; set; }
    public DateTime RegistrationDateTime { get; set; }
}
