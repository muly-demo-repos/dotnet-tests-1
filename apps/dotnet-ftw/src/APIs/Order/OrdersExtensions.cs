using DotnetFtw.APIs.Dtos;
using DotnetFtw.Infrastructure.Models;

namespace DotnetFtw.APIs.Extensions;

public static class OrdersExtensions
{
    public static OrderDto ToDto(this Order model)
    {
        return new OrderDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Status = model.Status,
            DatePlaced = model.DatePlaced,
            TotalAmount = model.TotalAmount,
            Customer = new CustomerIdDto { Id = model.CustomerId },
            Items = model.Items.Select(x => new ItemIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Order ToModel(this OrderUpdateInput updateDto, OrderIdDto idDto)
    {
        var order = new Order
        {
            Id = idDto.Id,
            Status = updateDto.Status,
            DatePlaced = updateDto.DatePlaced,
            TotalAmount = updateDto.TotalAmount
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            order.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            order.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return order;
    }
}
