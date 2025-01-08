using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Infrastructure.Models;

[Table("Guests")]
public class GuestDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [StringLength(1000)]
    public string? PhoneNumber { get; set; }

    [StringLength(1000)]
    public string? RoomNumber { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
