namespace DotnetFtw.APIs.Dtos;

public class OrderUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public DateTime? DatePlaced { get; set; }

    public double? TotalAmount { get; set; }

    public CustomerIdDto? Customer { get; set; }

    public List<ItemIdDto>? Items { get; set; }
}
