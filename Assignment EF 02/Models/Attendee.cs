namespace Assignment_EF_02.Models;

public class Attendee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;

    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public Badge? Badge { get; set; }
    public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
}
