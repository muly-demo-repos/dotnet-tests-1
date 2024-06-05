using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
