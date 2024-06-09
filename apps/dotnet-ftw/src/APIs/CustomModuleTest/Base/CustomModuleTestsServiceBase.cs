using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.Core.Enums;
using DotnetFtw.Infrastructure;
using DotnetFtw.Infrastructure.Models;

namespace DotnetFtw.APIs;

public abstract class CustomModuleTestsServiceBase : ICustomModuleTestsService
{
    protected readonly DotnetFtwDbContext _context;

    public CustomModuleTestsServiceBase(DotnetFtwDbContext context)
    {
        _context = context;
    }

    public async Task<CustomModuleCustomEnumTwoEnum> CustomModuleCustomAction1(
        CustomModuleCustomDtoOne customModuleCustomDtoOne
    )
    {
        throw new NotImplementedException();
    }

    public async Task<OrderAnotherCustomEnumEnum> CustomModuleCustomActionPrimitive(string data)
    {
        throw new NotImplementedException();
    }
}
