using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomModuleTestsControllerBase : ControllerBase
{
    protected readonly ICustomModuleTestsService _service;

    public CustomModuleTestsControllerBase(ICustomModuleTestsService service)
    {
        _service = service;
    }

    [HttpPost("{Id}/custom-module-custom-action-1")]
    public async Task<CustomModuleCustomEnumTwoEnum> CustomModuleCustomAction1(
        [FromQuery()] CustomModuleCustomDtoOne customModuleCustomDtoOne
    )
    {
        return await _service.CustomModuleCustomAction1(customModuleCustomDtoOne);
    }

    [HttpPatch("{Id}/custom-module-custom-action-primitive")]
    public async Task<OrderAnotherCustomEnumEnum> CustomModuleCustomActionPrimitive(
        [FromQuery()] string data
    )
    {
        return await _service.CustomModuleCustomActionPrimitive(data);
    }
}
