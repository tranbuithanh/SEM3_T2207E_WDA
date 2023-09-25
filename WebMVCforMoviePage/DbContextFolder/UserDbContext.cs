using Microsoft.EntityFrameworkCore;
using WebMVCforMoviePage.Models;

namespace WebMVCforMoviePage.DbContextFolder
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() 
        {
        }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MovieModel> MovieModel { get; set; }
        public DbSet<CategoryModel> CategoryModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<MovieModel>().ToTable("movie");
            modelBuilder.Entity<CategoryModel>().ToTable("category")
                .HasMany(category => category.Movies)
                .WithOne(movie => movie.Category)
                .HasForeignKey(movie => movie.CategoryId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = "Server=localhost,1433;Database=FPT_SEM3;User Id=sa;Password=Hoilamgi#201092;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
