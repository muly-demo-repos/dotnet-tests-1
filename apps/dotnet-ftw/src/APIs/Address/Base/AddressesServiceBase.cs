using DotnetFtw.APIs;
using DotnetFtw.APIs.Common;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using DotnetFtw.APIs.Extensions;
using DotnetFtw.Infrastructure;
using DotnetFtw.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFtw.APIs;

public abstract class AddressesServiceBase : IAddressesService
{
    protected readonly DotnetFtwDbContext _context;

    public AddressesServiceBase(DotnetFtwDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a Customer record for Address
    /// </summary>
    public async Task<CustomerDto> GetCustomer(AddressIdDto idDto)
    {
        var address = await _context
            .Addresses.Where(address => address.Id == idDto.Id)
            .Include(address => address.Customer)
            .FirstOrDefaultAsync();
        if (address == null)
        {
            throw new NotFoundException();
        }
        return address.Customer.ToDto();
    }

    /// <summary>
    /// Create one Address
    /// </summary>
    public async Task<AddressDto> CreateAddress(AddressCreateInput createDto)
    {
        var address = new Address
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            PostalCode = createDto.PostalCode,
            Street = createDto.Street,
            City = createDto.City,
            State = createDto.State,
            Country = createDto.Country
        };

        if (createDto.Id != null)
        {
            address.Id = createDto.Id;
        }

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Address>(address.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Address
    /// </summary>
    public async Task DeleteAddress(AddressIdDto idDto)
    {
        var address = await _context.Addresses.FindAsync(idDto.Id);
        if (address == null)
        {
            throw new NotFoundException();
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Addresses
    /// </summary>
    public async Task<List<AddressDto>> Addresses(AddressFindMany findManyArgs)
    {
        var addresses = await _context
            .Addresses.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return addresses.ConvertAll(address => address.ToDto());
    }

    /// <summary>
    /// Get one Address
    /// </summary>
    public async Task<AddressDto> Address(AddressIdDto idDto)
    {
        var addresses = await this.Addresses(
            new AddressFindMany { Where = new AddressWhereInput { Id = idDto.Id } }
        );
        var address = addresses.FirstOrDefault();
        if (address == null)
        {
            throw new NotFoundException();
        }

        return address;
    }

    /// <summary>
    /// Update one Address
    /// </summary>
    public async Task UpdateAddress(AddressIdDto idDto, AddressUpdateInput updateDto)
    {
        var address = updateDto.ToModel(idDto);

        _context.Entry(address).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Addresses.Any(e => e.Id == address.Id))
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
