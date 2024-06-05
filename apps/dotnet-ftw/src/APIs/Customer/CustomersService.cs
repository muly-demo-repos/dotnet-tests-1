using DotnetFtw.Infrastructure;

namespace DotnetFtw.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(DotnetFtwDbContext context)
        : base(context) { }
}
