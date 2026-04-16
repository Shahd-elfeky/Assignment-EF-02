namespace Assignment_EF_02.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxAttendees { get; set; }

    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; } = null!;

    public int? ParentEventId { get; set; }
    public Event? ParentEvent { get; set; }
    public ICollection<Event> Sessions { get; set; } = new List<Event>();

    public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
}
