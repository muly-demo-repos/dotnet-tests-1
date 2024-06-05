using DotnetFtw.APIs.Dtos;
using DotnetFtw.Infrastructure.Models;

namespace DotnetFtw.APIs.Extensions;

public static class ItemsExtensions
{
    public static ItemDto ToDto(this Item model)
    {
        return new ItemDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Description = model.Description,
            Sku = model.Sku,
            Name = model.Name,
            Price = model.Price,
            StockQuantity = model.StockQuantity,
            Orders = model.Orders.Select(x => new OrderIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Item ToModel(this ItemUpdateInput updateDto, ItemIdDto idDto)
    {
        var item = new Item
        {
            Id = idDto.Id,
            Description = updateDto.Description,
            Sku = updateDto.Sku,
            Name = updateDto.Name,
            Price = updateDto.Price,
            StockQuantity = updateDto.StockQuantity
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            item.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            item.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return item;
    }
}
