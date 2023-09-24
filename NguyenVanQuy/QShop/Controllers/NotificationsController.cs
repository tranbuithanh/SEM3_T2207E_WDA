using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Controllers
{
	public class NotificationsController : Controller
	{
		private readonly QShopContext _context;

		public NotificationsController(QShopContext context)
		{
			_context = context;
		}

		// GET: Notifications
		public async Task<IActionResult> Index()
		{
			int userId = 0;
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				var notifications = _context?.Notification?.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt).ToList();
				return View(notifications);
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}
		}

		// GET: Notifications
		public IActionResult Edit(int id)
		{
			int userId = 0;
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				var notification = _context?.Notification?.Where(n => n.UserId == userId && n.Id == id).FirstOrDefault();
				if (notification != null)
				{
					if (notification.Status == "read")
					{
						return Ok("not change");
					}
					notification.Status = "read";
					_context?.Update(notification);
					_context?.SaveChanges();
					return Ok("Ok");
				}
				else
				{
					return Ok("Not found");
				}
			}
			else
			{
				return Ok("Login");
			}
		}

	}
}
