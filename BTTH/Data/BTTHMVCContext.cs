using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BTTH.Models;

namespace BTTH.Data
{
    public class BTTHMVCContext : DbContext
    {
       
        public BTTHMVCContext (DbContextOptions<BTTHMVCContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BTTH.Models.Student>().ToTable("Students");//tblUser là tên bảng bên SQL Server
            modelBuilder.Entity<BTTH.Models.ClassCourse>().ToTable("ClassCourse");
            modelBuilder.Entity<BTTH.Models.STDbio>().ToTable("STDbio");
            modelBuilder.Entity<BTTH.Models.SubjectCls>().ToTable("SubjectCls");
         

        }
        public DbSet<BTTH.Models.User> User { get; set; } = default!;

        public DbSet<BTTH.Models.ClassCourse> ClassCourse { get; set; } = default!;

        public DbSet<BTTH.Models.SubjectCls> SubjectCls { get; set; } = default!;

        public DbSet<BTTH.Models.Student> Student { get; set; } = default!;

        public DbSet<BTTH.Models.STDbio> STDbio { get; set; } = default!;
    }
}
