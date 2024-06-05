using DotnetFtw.APIs;
using DotnetFtw.APIs.Common;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using DotnetFtw.APIs.Extensions;
using DotnetFtw.Infrastructure;
using DotnetFtw.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFtw.APIs;

public abstract class ItemsServiceBase : IItemsService
{
    protected readonly DotnetFtwDbContext _context;

    public ItemsServiceBase(DotnetFtwDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Item
    /// </summary>
    public async Task<ItemDto> CreateItem(ItemCreateInput createDto)
    {
        var item = new Item
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Description = createDto.Description,
            Sku = createDto.Sku,
            Name = createDto.Name,
            Price = createDto.Price,
            StockQuantity = createDto.StockQuantity
        };

        if (createDto.Id != null)
        {
            item.Id = createDto.Id;
        }
        if (createDto.Orders != null)
        {
            item.Orders = await _context
                .Orders.Where(order => createDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Item>(item.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    public async Task DeleteItem(ItemIdDto idDto)
    {
        var item = await _context.Items.FindAsync(idDto.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Items
    /// </summary>
    public async Task<List<ItemDto>> Items(ItemFindMany findManyArgs)
    {
        var items = await _context
            .Items.Include(x => x.Orders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return items.ConvertAll(item => item.ToDto());
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    public async Task<ItemDto> Item(ItemIdDto idDto)
    {
        var items = await this.Items(
            new ItemFindMany { Where = new ItemWhereInput { Id = idDto.Id } }
        );
        var item = items.FirstOrDefault();
        if (item == null)
        {
            throw new NotFoundException();
        }

        return item;
    }

    /// <summary>
    /// Connect multiple Orders records to Item
    /// </summary>
    public async Task ConnectOrders(ItemIdDto idDto, OrderIdDto[] ordersId)
    {
        var item = await _context
            .Items.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        var orders = await _context
            .Orders.Where(t => ordersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (orders.Count == 0)
        {
            throw new NotFoundException();
        }

        var ordersToConnect = orders.Except(item.Orders);

        foreach (var order in ordersToConnect)
        {
            item.Orders.Add(order);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Item
    /// </summary>
    public async Task DisconnectOrders(ItemIdDto idDto, OrderIdDto[] ordersId)
    {
        var item = await _context
            .Items.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        var orders = await _context
            .Orders.Where(t => ordersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var order in orders)
        {
            item.Orders?.Remove(order);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Orders records for Item
    /// </summary>
    public async Task<List<OrderDto>> FindOrders(ItemIdDto idDto, OrderFindMany itemFindMany)
    {
        var orders = await _context
            .Orders.Where(m => m.Items.Any(x => x.Id == idDto.Id))
            .ApplyWhere(itemFindMany.Where)
            .ApplySkip(itemFindMany.Skip)
            .ApplyTake(itemFindMany.Take)
            .ApplyOrderBy(itemFindMany.SortBy)
            .ToListAsync();

        return orders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Orders records for Item
    /// </summary>
    public async Task UpdateOrders(ItemIdDto idDto, OrderIdDto[] ordersId)
    {
        var item = await _context
            .Items.Include(t => t.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        var orders = await _context
            .Orders.Where(a => ordersId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (orders.Count == 0)
        {
            throw new NotFoundException();
        }

        item.Orders = orders;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Item
    /// </summary>
    public async Task UpdateItem(ItemIdDto idDto, ItemUpdateInput updateDto)
    {
        var item = updateDto.ToModel(idDto);

        if (updateDto.Orders != null)
        {
            item.Orders = await _context
                .Orders.Where(order => updateDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Items.Any(e => e.Id == item.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
