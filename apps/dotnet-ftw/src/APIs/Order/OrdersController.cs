using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
