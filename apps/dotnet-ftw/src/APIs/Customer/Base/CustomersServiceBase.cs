using DotnetFtw.APIs;
using DotnetFtw.APIs.Common;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using DotnetFtw.APIs.Extensions;
using DotnetFtw.Infrastructure;
using DotnetFtw.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFtw.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly DotnetFtwDbContext _context;

    public CustomersServiceBase(DotnetFtwDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<CustomerDto> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new Customer
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Birthdate = createDto.Birthdate,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Orders != null)
        {
            customer.Orders = await _context
                .Orders.Where(order => createDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Customer>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Connect multiple Orders records to Customer
    /// </summary>
    public async Task ConnectOrders(CustomerIdDto idDto, OrderIdDto[] ordersId)
    {
        var customer = await _context
            .Customers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
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

        var ordersToConnect = orders.Except(customer.Orders);

        foreach (var order in ordersToConnect)
        {
            customer.Orders.Add(order);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Customer
    /// </summary>
    public async Task DisconnectOrders(CustomerIdDto idDto, OrderIdDto[] ordersId)
    {
        var customer = await _context
            .Customers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var orders = await _context
            .Orders.Where(t => ordersId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var order in orders)
        {
            customer.Orders?.Remove(order);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Orders records for Customer
    /// </summary>
    public async Task<List<OrderDto>> FindOrders(
        CustomerIdDto idDto,
        OrderFindMany customerFindMany
    )
    {
        var orders = await _context
            .Orders.Where(m => m.CustomerId == idDto.Id)
            .ApplyWhere(customerFindMany.Where)
            .ApplySkip(customerFindMany.Skip)
            .ApplyTake(customerFindMany.Take)
            .ApplyOrderBy(customerFindMany.SortBy)
            .ToListAsync();

        return orders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Get a address record for Customer
    /// </summary>
    public async Task<AddressDto> GetAddress(CustomerIdDto idDto)
    {
        var customer = await _context
            .Customers.Where(customer => customer.Id == idDto.Id)
            .Include(customer => customer.Address)
            .FirstOrDefaultAsync();
        if (customer == null)
        {
            throw new NotFoundException();
        }
        return customer.Address.ToDto();
    }

    /// <summary>
    /// Update multiple Orders records for Customer
    /// </summary>
    public async Task UpdateOrders(CustomerIdDto idDto, OrderIdDto[] ordersId)
    {
        var customer = await _context
            .Customers.Include(t => t.Orders)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
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

        customer.Orders = orders;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerIdDto idDto)
    {
        var customer = await _context.Customers.FindAsync(idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Address)
            .Include(x => x.Orders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<CustomerDto> Customer(CustomerIdDto idDto)
    {
        var customers = await this.Customers(
            new CustomerFindMany { Where = new CustomerWhereInput { Id = idDto.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto)
    {
        var customer = updateDto.ToModel(idDto);

        if (updateDto.Orders != null)
        {
            customer.Orders = await _context
                .Orders.Where(order => updateDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
