namespace DotnetFtw.APIs.Dtos;

public class ItemDto : ItemIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Description { get; set; }

    public string? Sku { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public int? StockQuantity { get; set; }

    public List<OrderIdDto>? Orders { get; set; }
}
