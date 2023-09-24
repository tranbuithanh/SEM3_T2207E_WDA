using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.SmallReviews
{
	public class SmallReviews : ViewComponent
	{
		private readonly QShopContext _context;
		public SmallReviews(QShopContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var reviews = _context.Review.Include(r => r.user).OrderByDescending(r => r.Rating).Take(3).ToList();
			return View(reviews);
		}
	}
}
