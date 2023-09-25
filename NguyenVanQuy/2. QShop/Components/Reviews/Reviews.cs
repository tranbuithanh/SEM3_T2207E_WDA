using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.Reviews
{
	public class Reviews : ViewComponent
	{
		private readonly QShopContext _context;
		public Reviews(QShopContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var reviews = _context.Review.Include(r => r.user).OrderByDescending(r => r.Rating).Take(10).ToList();
			return View(reviews);
		}
	}
}
