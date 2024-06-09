using DotnetFtw.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

public class CustomModuleTestsService : CustomModuleTestsServiceBase
{
    public CustomModuleTestsService(DotnetFtwDbContext context)
        : base(context) { }
}
