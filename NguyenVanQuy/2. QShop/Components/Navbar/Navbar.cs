using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.Navbar
{
	public class Navbar : ViewComponent
	{
		private readonly QShopContext _context;
		public Navbar(QShopContext context)
		{
			_context = context;
		}


		public IViewComponentResult Invoke()
		{
			var banner = _context.Banner?.FirstOrDefault(b => b.Status == "on");
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				ViewData["Login"] = true;
				var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
				ViewData["UserId"] = userId;
				if (userId != null)
				{
					var user = _context.User.FirstOrDefault(u => u.Id == Int32.Parse(userId));
					var carts = _context.Cart?.Where(c => c.UserId == Int32.Parse(userId)).ToList();
					var notifications = _context.Notification?.Where(n => n.UserId == Int32.Parse(userId) && n.Status == "unread").ToList();
					ViewData["Role"] = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
					ViewData["Name"] = User.Identity?.Name;
					ViewData["CartsCount"] = carts?.Count();
					ViewData["NotificationsCount"] = notifications?.Count();
					ViewData["Thumbnail"] = user?.Thumbnail;
					ViewData["Balance"] = user?.Balance;
				}
			}
			else
			{
				ViewData["Login"] = false;
			}
			ViewData["Banner"] = banner?.Content ?? "";
			return View();

		}
	}
}
