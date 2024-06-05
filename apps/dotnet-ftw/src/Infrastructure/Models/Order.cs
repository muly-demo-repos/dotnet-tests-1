using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetFtw.Infrastructure.Models;

[Table("Orders")]
public class Order
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Status { get; set; }

    public DateTime? DatePlaced { get; set; }

    [Range(-999999999, 999999999)]
    public double? TotalAmount { get; set; }

    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; } = null;

    public List<Item>? Items { get; set; } = new List<Item>();
}
