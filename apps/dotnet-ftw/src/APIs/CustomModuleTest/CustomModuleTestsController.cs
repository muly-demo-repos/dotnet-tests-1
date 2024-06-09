using DotnetFtw.APIs;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[ApiController()]
public class CustomModuleTestsController : CustomModuleTestsControllerBase
{
    public CustomModuleTestsController(ICustomModuleTestsService service)
        : base(service) { }
}
