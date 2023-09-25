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

        public DbSet<BTTH.Models.User> User { get; set; } = default!;

        public DbSet<BTTH.Models.ClassCourse> ClassCourse { get; set; } = default!;

        public DbSet<BTTH.Models.SubjectCls> SubjectCls { get; set; } = default!;

        public DbSet<BTTH.Models.Student> Student { get; set; } = default!;

        public DbSet<BTTH.Models.STDbio> STDbio { get; set; } = default!;
    }
}
