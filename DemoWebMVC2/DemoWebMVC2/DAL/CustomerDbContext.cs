using System;
using Microsoft.EntityFrameworkCore;
using DemoWebMVC2.Models;

namespace DemoWebMVC2.DAL
{
	public class CustomerDbContext:DbContext
	{
		public CustomerDbContext():base()
		{
		}
		public CustomerDbContext(DbContextOptions<CustomerDbContext> options):base(options)
		{ }
		public DbSet<Customer> Customers { get; set;   }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}

