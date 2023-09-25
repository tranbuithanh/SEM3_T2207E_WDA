using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Areas.Admin.Components.NavbarAdmin
{
	public class NavbarAdmin : ViewComponent
	{
		private readonly QShopContext _context;
		public NavbarAdmin(QShopContext context)
		{
			_context = context;
		}


		public IViewComponentResult Invoke()
		{
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				ViewData["Login"] = true;
				var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
				ViewData["UserId"] = userId;
				if (userId != null)
				{
					var user = _context.User.FirstOrDefault(u => u.Id == Int32.Parse(userId));
					var carts = _context.Cart?.Where(c => c.UserId == Int32.Parse(userId)).ToList();
					ViewData["Role"] = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
					ViewData["Name"] = User.Identity?.Name;
					ViewData["CartsCount"] = carts?.Count() > 9 ? "9+" : carts?.Count();
					ViewData["Thumbnail"] = user?.Thumbnail;
					ViewData["Balance"] = user?.Balance;
				}

			}
			else
			{
				ViewData["Login"] = false;
			}
			return View();
		}
	}
}
