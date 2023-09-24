using Exam2.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam2.DAL
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductChapter>().ToTable("ProductChapter");
            modelBuilder.Entity<ProductChapterImage>().ToTable("ProductChapterImage");
        }

        DbSet<Author> authors;
        DbSet<Category> categories;
        DbSet<Product> products;
        DbSet<ProductChapter> productChapters;
        DbSet<ProductChapterImage> productChaptersImages;
        public DbSet<Exam2.Models.Author> Author { get; set; } = default!;
        public DbSet<Exam2.Models.Category> Category { get; set; } = default!;
        public DbSet<Exam2.Models.Product> Product { get; set; } = default!;
        public DbSet<Exam2.Models.ProductChapter> ProductChapter { get; set; } = default!;
        public DbSet<Exam2.Models.ProductChapterImage> ProductChapterImage { get; set; } = default!;
    }
}
