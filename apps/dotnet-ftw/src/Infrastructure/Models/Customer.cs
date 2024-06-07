using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetFtw.Infrastructure.Models;

[Table("Customers")]
public class Customer
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public DateTime? Birthdate { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? PhoneNumber { get; set; }

    public Address? Address { get; set; } = null;

    public List<Order>? Orders { get; set; } = new List<Order>();
}
