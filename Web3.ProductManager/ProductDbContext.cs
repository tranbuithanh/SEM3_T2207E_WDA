using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Web3.ProductManager.Models;

namespace Web3.ProductManager
{
	public class ProductDbContext:DbContext
	{
        public ProductDbContext():base()
        {

        }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        public DbSet<ProductModel> Products;
        public DbSet<StoreModel> Stores;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SEM3_WDA_PMS;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductModel>().ToTable("Product");
            modelBuilder.Entity<StoreModel>().ToTable("Store");
        }

        public DbSet<Web3.ProductManager.Models.ProductModel> ProductModel { get; set; } = default!;
    }
}

