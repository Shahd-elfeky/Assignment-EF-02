namespace Assignment_EF_02.Models;

public class Badge
{
    public int Id { get; set; }
    public string CredentialNumber { get; set; } = null!;
    public DateTime IssuedDate { get; set; }
    public string Tier { get; set; } = null!;

    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; } = null!;
}
