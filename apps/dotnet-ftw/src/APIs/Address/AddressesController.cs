using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[ApiController()]
public class AddressesController : AddressesControllerBase
{
    public AddressesController(IAddressesService service)
        : base(service) { }
}
