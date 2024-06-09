using DotnetFtw.APIs.Dtos;
using DotnetFtw.Core.Enums;

namespace DotnetFtw.APIs;

public interface IOrdersService
{
    /// <summary>
    /// Create one Order
    /// </summary>
    public Task<OrderDto> CreateOrder(OrderCreateInput orderDto);

    /// <summary>
    /// Delete one Order
    /// </summary>
    public Task DeleteOrder(OrderIdDto idDto);

    /// <summary>
    /// Find many Orders
    /// </summary>
    public Task<List<OrderDto>> Orders(OrderFindMany findManyArgs);

    /// <summary>
    /// Get one Order
    /// </summary>
    public Task<OrderDto> Order(OrderIdDto idDto);

    /// <summary>
    /// Connect multiple Items records to Order
    /// </summary>
    public Task ConnectItems(OrderIdDto idDto, ItemIdDto[] itemsId);
    public Task<OrderAnotherCustomEnumEnum> OrderCustomAction(OrderCustomDto orderCustomDto);
    public Task<int> OrderCustomActionWithPrimitives(string data);

    /// <summary>
    /// Disconnect multiple Items records from Order
    /// </summary>
    public Task DisconnectItems(OrderIdDto idDto, ItemIdDto[] itemsId);

    /// <summary>
    /// Find multiple Items records for Order
    /// </summary>
    public Task<List<ItemDto>> FindItems(OrderIdDto idDto, ItemFindMany ItemFindMany);

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    public Task<CustomerDto> GetCustomer(OrderIdDto idDto);

    /// <summary>
    /// Update multiple Items records for Order
    /// </summary>
    public Task UpdateItems(OrderIdDto idDto, ItemIdDto[] itemsId);

    /// <summary>
    /// Update one Order
    /// </summary>
    public Task UpdateOrder(OrderIdDto idDto, OrderUpdateInput updateDto);
}
