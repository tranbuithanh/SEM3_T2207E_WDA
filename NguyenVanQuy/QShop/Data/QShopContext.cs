using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QShop.Models;

namespace QShop.Data
{
	public class QShopContext : DbContext
	{
		public QShopContext(DbContextOptions<QShopContext> options)
				: base(options)
		{
		}

		public DbSet<QShop.Models.User> User { get; set; } = default!;

		public DbSet<QShop.Models.Account>? Account { get; set; }

		public DbSet<QShop.Models.Rank>? Rank { get; set; }

		public DbSet<QShop.Models.Game>? Game { get; set; }

		public DbSet<QShop.Models.Cart>? Cart { get; set; }
		public DbSet<QShop.Models.Review>? Review { get; set; }
		public DbSet<QShop.Models.Coupon>? Coupon { get; set; }
		public DbSet<QShop.Models.Invoice>? Invoice { get; set; }
		public DbSet<QShop.Models.InvoiceDetail>? InvoiceDetail { get; set; }
		public DbSet<QShop.Models.Notification>? Notification { get; set; }
		public DbSet<QShop.Models.Article>? Article { get; set; }
		public DbSet<QShop.Models.Banner>? Banner { get; set; }


		// protected override void OnModelCreating(ModelBuilder modelBuilder)
		// {
		// 	modelBuilder.Entity<Game>()
		// 			.HasMany(g => g.accounts)
		// 			.WithOne(a => a.game)
		// 			.HasForeignKey(a => a.GameId);
		// }

	}
}
