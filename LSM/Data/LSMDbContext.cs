using LSM.Models;
using Microsoft.EntityFrameworkCore;
using LSM.Models;
namespace LSM.Data
{
    public class LSMDbContext : DbContext
    {
        public LSMDbContext(DbContextOptions<LSMDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Member>().ToTable("Member");

        }
        DbSet<Book> Books;
        DbSet<Category> Categories;
        DbSet<Member> Members;
        public DbSet<LSM.Models.Book> Book { get; set; } = default!;
        public DbSet<LSM.Models.Category> Category { get; set; } = default!;
        public DbSet<LSM.Models.Member> Member { get; set; } = default!;
    }
}
