using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetFtw.Infrastructure.Models;

[Table("Items")]
public class Item
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(1000)]
    public string? Sku { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    [Range(-999999999, 999999999)]
    public int? StockQuantity { get; set; }

    public List<Order>? Orders { get; set; } = new List<Order>();
}
