using DotnetFtw.APIs.Dtos;

namespace DotnetFtw.APIs;

public interface IItemsService
{
    /// <summary>
    /// Create one Item
    /// </summary>
    public Task<ItemDto> CreateItem(ItemCreateInput itemDto);

    /// <summary>
    /// Delete one Item
    /// </summary>
    public Task DeleteItem(ItemIdDto idDto);

    /// <summary>
    /// Find many Items
    /// </summary>
    public Task<List<ItemDto>> Items(ItemFindMany findManyArgs);

    /// <summary>
    /// Get one Item
    /// </summary>
    public Task<ItemDto> Item(ItemIdDto idDto);

    /// <summary>
    /// Connect multiple Orders records to Item
    /// </summary>
    public Task ConnectOrders(ItemIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Disconnect multiple Orders records from Item
    /// </summary>
    public Task DisconnectOrders(ItemIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Find multiple Orders records for Item
    /// </summary>
    public Task<List<OrderDto>> FindOrders(ItemIdDto idDto, OrderFindMany OrderFindMany);

    /// <summary>
    /// Update multiple Orders records for Item
    /// </summary>
    public Task UpdateOrders(ItemIdDto idDto, OrderIdDto[] ordersId);

    /// <summary>
    /// Update one Item
    /// </summary>
    public Task UpdateItem(ItemIdDto idDto, ItemUpdateInput updateDto);
}
