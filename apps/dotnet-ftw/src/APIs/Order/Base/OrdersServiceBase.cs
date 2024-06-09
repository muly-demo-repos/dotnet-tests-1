using DotnetFtw.APIs;
using DotnetFtw.APIs.Common;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using DotnetFtw.APIs.Extensions;
using DotnetFtw.Core.Enums;
using DotnetFtw.Infrastructure;
using DotnetFtw.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFtw.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly DotnetFtwDbContext _context;

    public OrdersServiceBase(DotnetFtwDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    public async Task<OrderDto> CreateOrder(OrderCreateInput createDto)
    {
        var order = new Order
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Status = createDto.Status,
            DatePlaced = createDto.DatePlaced,
            TotalAmount = createDto.TotalAmount
        };

        if (createDto.Id != null)
        {
            order.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            order.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Items != null)
        {
            order.Items = await _context
                .Items.Where(item => createDto.Items.Select(t => t.Id).Contains(item.Id))
                .ToListAsync();
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Order>(order.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    public async Task DeleteOrder(OrderIdDto idDto)
    {
        var order = await _context.Orders.FindAsync(idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    public async Task<List<OrderDto>> Orders(OrderFindMany findManyArgs)
    {
        var orders = await _context
            .Orders.Include(x => x.Customer)
            .Include(x => x.Items)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orders.ConvertAll(order => order.ToDto());
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    public async Task<OrderDto> Order(OrderIdDto idDto)
    {
        var orders = await this.Orders(
            new OrderFindMany { Where = new OrderWhereInput { Id = idDto.Id } }
        );
        var order = orders.FirstOrDefault();
        if (order == null)
        {
            throw new NotFoundException();
        }

        return order;
    }

    /// <summary>
    /// Connect multiple Items records to Order
    /// </summary>
    public async Task ConnectItems(OrderIdDto idDto, ItemIdDto[] itemsId)
    {
        var order = await _context
            .Orders.Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var items = await _context
            .Items.Where(t => itemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (items.Count == 0)
        {
            throw new NotFoundException();
        }

        var itemsToConnect = items.Except(order.Items);

        foreach (var item in itemsToConnect)
        {
            order.Items.Add(item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<OrderAnotherCustomEnumEnum> OrderCustomAction(OrderCustomDto orderCustomDto)
    {
        throw new NotImplementedException();
    }

    public async Task<int> OrderCustomActionWithPrimitives(string data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Disconnect multiple Items records from Order
    /// </summary>
    public async Task DisconnectItems(OrderIdDto idDto, ItemIdDto[] itemsId)
    {
        var order = await _context
            .Orders.Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var items = await _context
            .Items.Where(t => itemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var item in items)
        {
            order.Items?.Remove(item);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Items records for Order
    /// </summary>
    public async Task<List<ItemDto>> FindItems(OrderIdDto idDto, ItemFindMany orderFindMany)
    {
        var items = await _context
            .Items.Where(m => m.Orders.Any(x => x.Id == idDto.Id))
            .ApplyWhere(orderFindMany.Where)
            .ApplySkip(orderFindMany.Skip)
            .ApplyTake(orderFindMany.Take)
            .ApplyOrderBy(orderFindMany.SortBy)
            .ToListAsync();

        return items.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    public async Task<CustomerDto> GetCustomer(OrderIdDto idDto)
    {
        var order = await _context
            .Orders.Where(order => order.Id == idDto.Id)
            .Include(order => order.Customer)
            .FirstOrDefaultAsync();
        if (order == null)
        {
            throw new NotFoundException();
        }
        return order.Customer.ToDto();
    }

    /// <summary>
    /// Update multiple Items records for Order
    /// </summary>
    public async Task UpdateItems(OrderIdDto idDto, ItemIdDto[] itemsId)
    {
        var order = await _context
            .Orders.Include(t => t.Items)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var items = await _context
            .Items.Where(a => itemsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (items.Count == 0)
        {
            throw new NotFoundException();
        }

        order.Items = items;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    public async Task UpdateOrder(OrderIdDto idDto, OrderUpdateInput updateDto)
    {
        var order = updateDto.ToModel(idDto);

        if (updateDto.Items != null)
        {
            order.Items = await _context
                .Items.Where(item => updateDto.Items.Select(t => t.Id).Contains(item.Id))
                .ToListAsync();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Orders.Any(e => e.Id == order.Id))
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
