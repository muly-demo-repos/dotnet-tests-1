using DotnetFtw.APIs.Dtos;

namespace DotnetFtw.APIs;

public interface IAddressesService
{
    /// <summary>
    /// Get a Customer record for Address
    /// </summary>
    public Task<CustomerDto> GetCustomer(AddressIdDto idDto);

    /// <summary>
    /// Create one Address
    /// </summary>
    public Task<AddressDto> CreateAddress(AddressCreateInput addressDto);

    /// <summary>
    /// Delete one Address
    /// </summary>
    public Task DeleteAddress(AddressIdDto idDto);

    /// <summary>
    /// Find many Addresses
    /// </summary>
    public Task<List<AddressDto>> Addresses(AddressFindMany findManyArgs);

    /// <summary>
    /// Get one Address
    /// </summary>
    public Task<AddressDto> Address(AddressIdDto idDto);

    /// <summary>
    /// Update one Address
    /// </summary>
    public Task UpdateAddress(AddressIdDto idDto, AddressUpdateInput updateDto);
}
