using Microsoft.EntityFrameworkCore;

namespace marketperry.Models;

public class applicationDbContext : DbContext
{
    public applicationDbContext(DbContextOptions<applicationDbContext> options) : base(options)
    {}
    public DbSet<account> accounts { get; set; }
    public DbSet<mobilephone> mobilephones { get; set; }
    public DbSet<AppleWatch> applewatchs { get; set; }
    public DbSet<Cart> carts { get; set; }
}