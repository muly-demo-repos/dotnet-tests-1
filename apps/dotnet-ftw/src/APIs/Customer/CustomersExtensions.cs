using DotnetFtw.APIs.Dtos;
using DotnetFtw.Infrastructure.Models;

namespace DotnetFtw.APIs.Extensions;

public static class CustomersExtensions
{
    public static CustomerDto ToDto(this Customer model)
    {
        return new CustomerDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Birthdate = model.Birthdate,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address?.ToDto(),
            Orders = model.Orders?.Select(x => new OrderIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Customer ToModel(this CustomerUpdateInput updateDto, CustomerIdDto idDto)
    {
        var customer = new Customer
        {
            Id = idDto.Id,
            Birthdate = updateDto.Birthdate,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Email = updateDto.Email,
            PhoneNumber = updateDto.PhoneNumber
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
