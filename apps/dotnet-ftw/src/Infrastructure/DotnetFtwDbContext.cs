using DotnetFtw.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFtw.Infrastructure;

public class DotnetFtwDbContext : DbContext
{
    public DotnetFtwDbContext(DbContextOptions<DotnetFtwDbContext> options)
        : base(options) { }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Order> Orders { get; set; }
}
