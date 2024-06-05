using DotnetFtw.APIs;

namespace DotnetFtw;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressesService, AddressesService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IOrdersService, OrdersService>();
    }
}
