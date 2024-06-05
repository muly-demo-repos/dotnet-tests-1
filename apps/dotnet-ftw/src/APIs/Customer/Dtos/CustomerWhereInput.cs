namespace DotnetFtw.APIs.Dtos;

public class CustomerWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public AddressIdDto? Address { get; set; }

    public List<OrderIdDto>? Orders { get; set; }
}
