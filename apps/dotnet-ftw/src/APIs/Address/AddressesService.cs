using DotnetFtw.Infrastructure;

namespace DotnetFtw.APIs;

public class AddressesService : AddressesServiceBase
{
    public AddressesService(DotnetFtwDbContext context)
        : base(context) { }
}
