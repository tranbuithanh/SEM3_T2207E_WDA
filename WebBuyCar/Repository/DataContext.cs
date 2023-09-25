using System;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Models;

namespace WebBuyCar.Repository
{
	public class DataContext:DbContext
	{
		public DataContext(DbContextOptions<DataContext> options): base(options)
		{

		}
		public DbSet<NewsModel> News { get; set; }
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<BannerModel> Banners { get; set; }
	}
}

