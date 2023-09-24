using System;
using Microsoft.EntityFrameworkCore;
using insuarance_TranThiMaiHien.Models;

namespace insuarance_TranThiMaiHien.Entities
{
	public class DataContext:DbContext
	{
		public DataContext(DbContextOptions<DataContext> options):base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post>  Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> messages { get; set; }
    }
}

