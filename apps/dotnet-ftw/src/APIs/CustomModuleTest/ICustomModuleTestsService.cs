using DotnetFtw.APIs.Dtos;
using DotnetFtw.Core.Enums;

namespace DotnetFtw.APIs;

public interface ICustomModuleTestsService
{
    public Task<CustomModuleCustomEnumTwoEnum> CustomModuleCustomAction1(
        CustomModuleCustomDtoOne customModuleCustomDtoOne
    );
    public Task<OrderAnotherCustomEnumEnum> CustomModuleCustomActionPrimitive(string data);
}
