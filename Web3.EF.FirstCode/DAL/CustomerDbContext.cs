using System;
using Microsoft.EntityFrameworkCore;
using Web3.EF.FirstCode.Models;

namespace Web3.EF.FirstCode.DAL
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {

        }
     
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost,1433;Database=SEM3_WDA_1st;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true";
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

