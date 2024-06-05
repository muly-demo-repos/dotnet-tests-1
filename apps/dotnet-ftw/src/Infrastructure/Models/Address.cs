using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetFtw.Infrastructure.Models;

[Table("Addresses")]
public class Address
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? PostalCode { get; set; }

    [StringLength(1000)]
    public string? Street { get; set; }

    [StringLength(1000)]
    public string? City { get; set; }

    [StringLength(1000)]
    public string? State { get; set; }

    [StringLength(1000)]
    public string? Country { get; set; }

    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; } = null;
}
