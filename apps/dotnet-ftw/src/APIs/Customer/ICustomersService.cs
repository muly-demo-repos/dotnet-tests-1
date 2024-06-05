using DotnetFtw.APIs.Dtos;

namespace DotnetFtw.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<CustomerDto> CreateCustomer(CustomerCreateInput customerDto);

    /// <summary>
    /// Connect multiple Orders records to Customer
    /// </summary>
    public Task ConnectOrders(CustomerIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Disconnect multiple Orders records from Customer
    /// </summary>
    public Task DisconnectOrders(CustomerIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Find multiple Orders records for Customer
    /// </summary>
    public Task<List<OrderDto>> FindOrders(CustomerIdDto idDto, OrderFindMany OrderFindMany);

    /// <summary>
    /// Get a address record for Customer
    /// </summary>
    public Task<AddressDto> GetAddress(CustomerIdDto idDto);

    /// <summary>
    /// Update multiple Orders records for Customer
    /// </summary>
    public Task UpdateOrders(CustomerIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerIdDto idDto);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<CustomerDto> Customer(CustomerIdDto idDto);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto);
}
