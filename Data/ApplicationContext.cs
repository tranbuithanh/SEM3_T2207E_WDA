using Microsoft.EntityFrameworkCore;
using S_Assignment.Models;

namespace S_Assignment.Data;

public class ApplicationContext: DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Course>().ToTable("Courses");
        modelBuilder.Entity<Class>().ToTable("Classes");
    }
}