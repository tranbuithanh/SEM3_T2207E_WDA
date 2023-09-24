using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CouponsController : Controller
	{
		private readonly QShopContext _context;

		public CouponsController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Games
		public async Task<IActionResult> Index()
		{
			var coupons = _context.Coupon?.ToList(); ;
			if (coupons != null)
			{
				return View(coupons);
			}
			return Problem("Entity set 'QShopContext.Account'  is null.");
		}

		// GET: Admin/Coupons/Create
		public IActionResult Create()
		{
			Coupon coupon = new Coupon();
			return View(coupon);
		}

		// POST: Admin/Coupons/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Coupon _coupon)
		{
			ModelState.Remove("Description");
			if (ModelState.IsValid)
			{
				if (_coupon.Description == null)
				{
					_coupon.Description = "Mã giảm giá " + _coupon.DiscountPercentage + "%";
				}
				_coupon.DiscountPercentage = _coupon.DiscountPercentage / 100; ;
				_context.Coupon.Add(_coupon);
				_context.SaveChanges();
				return Redirect("/admin/coupons");
			}
			return View(_coupon);
		}

		// GET: Admin/Coupons/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Coupon == null)
			{
				return NotFound();
			}

			var coupon = await _context.Coupon.FindAsync(id);
			if (coupon == null)
			{
				return NotFound();
			}
			return View(coupon);
		}

		// POST: Admin/Coupons/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Coupon _coupon)
		{
			ModelState.Remove("Description");
			if (ModelState.IsValid)
			{
				if (_coupon.Description == null)
				{
					_coupon.Description = "Mã giảm giá " + _coupon.DiscountPercentage + "%";
				}
				_coupon.DiscountPercentage = _coupon.DiscountPercentage / 100; ;
				_context.Coupon.Update(_coupon);
				_context.SaveChanges();
				return Redirect("/admin/coupons");
			}
			return View(_coupon);
		}

		// GET: Admin/Coupons/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null || _context.Coupon == null)
			{
				return NotFound();
			}
			var coupon = _context.Coupon?.FirstOrDefault(m => m.Id == id);
			if (coupon == null)
			{
				return NotFound();
			}
			_context.Remove(coupon);
			_context.SaveChanges();
			return Ok("200");
		}

		private bool CouponExists(int id)
		{
			return (_context.Coupon?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
