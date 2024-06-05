using DotnetFtw.Infrastructure;

namespace DotnetFtw.APIs;

public class ItemsService : ItemsServiceBase
{
    public ItemsService(DotnetFtwDbContext context)
        : base(context) { }
}
