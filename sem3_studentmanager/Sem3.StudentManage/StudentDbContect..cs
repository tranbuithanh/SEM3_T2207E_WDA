using System;
using Microsoft.EntityFrameworkCore;
using Sem3.StudentManage.Models;

namespace Sem3.StudentManage
{
	public class StudentDbContect:DbContext
	{
		public StudentDbContect(DbContextOptions<StudentDbContect>options):base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentModel>().ToTable("Student");
        }
        DbSet<StudentModel> StudentModels;
        public DbSet<Sem3.StudentManage.Models.StudentModel> StudentModel { get; set; } = default!;
    }
}

