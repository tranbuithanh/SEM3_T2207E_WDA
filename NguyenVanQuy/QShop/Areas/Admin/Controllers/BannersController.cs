using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class BannersController : Controller
	{
		private readonly QShopContext _context;

		public BannersController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Banners
		public IActionResult Index()
		{
			var banner = _context?.Banner?.FirstOrDefault();
			if (banner != null)
			{
				return View(banner);
			}
			else
			{
				return Ok("Không có banner nào!");
			}
		}


		// POST: Admin/Banners/Edit
		[HttpPost]
		public IActionResult Edit(int? id, Banner _banner)
		{
			if (id != _banner.Id)
			{
				return NotFound();
			}
			_context?.Banner?.Update(_banner);
			_context?.SaveChanges();
			return Redirect("/admin/Banners");
		}

	}
}
