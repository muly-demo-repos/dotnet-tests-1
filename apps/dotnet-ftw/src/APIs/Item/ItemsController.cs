using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[ApiController()]
public class ItemsController : ItemsControllerBase
{
    public ItemsController(IItemsService service)
        : base(service) { }
}
