using DotnetFtw.APIs.Dtos;
using DotnetFtw.Infrastructure.Models;

namespace DotnetFtw.APIs.Extensions;

public static class AddressesExtensions
{
    public static AddressDto ToDto(this Address model)
    {
        return new AddressDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            PostalCode = model.PostalCode,
            Street = model.Street,
            City = model.City,
            State = model.State,
            Country = model.Country,
            Customer = new CustomerIdDto { Id = model.CustomerId },
        };
    }

    public static Address ToModel(this AddressUpdateInput updateDto, AddressIdDto idDto)
    {
        var address = new Address
        {
            Id = idDto.Id,
            PostalCode = updateDto.PostalCode,
            Street = updateDto.Street,
            City = updateDto.City,
            State = updateDto.State,
            Country = updateDto.Country
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            address.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            address.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return address;
    }
}
