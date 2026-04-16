using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_EF_02.Models;

public class OrganizerProfile
{
    [Key]
    public int Id { get; set; }

    [MaxLength(500)]
    public string Biography { get; set; } = string.Empty;

    [MaxLength(200)]
    public string WebsiteLink { get; set; } = string.Empty;

    public string LogoPath { get; set; } = string.Empty;

    // Required relation
    [Required]
    [ForeignKey("Organizer")]
    public int OrganizerId { get; set; }

    public Organizer Organizer { get; set; } = null!;
}
