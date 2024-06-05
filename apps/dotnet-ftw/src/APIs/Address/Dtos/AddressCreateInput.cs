namespace DotnetFtw.APIs.Dtos;

public class AddressCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? PostalCode { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public CustomerIdDto? Customer { get; set; }
}
