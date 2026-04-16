using System.ComponentModel.DataAnnotations;

namespace Assignment_EF_02.Models;

public class Organizer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? CompanyName { get; set; }

    public bool IsVerified { get; set; }

    public OrganizerProfile Profile { get; set; } = null!;
}
