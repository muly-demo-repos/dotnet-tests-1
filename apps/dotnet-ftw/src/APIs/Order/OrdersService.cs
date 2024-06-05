using DotnetFtw.Infrastructure;

namespace DotnetFtw.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(DotnetFtwDbContext context)
        : base(context) { }
}
