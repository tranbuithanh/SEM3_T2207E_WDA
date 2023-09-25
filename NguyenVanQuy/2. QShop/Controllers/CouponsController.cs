using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;
using System;
using System.Linq.Expressions;

namespace QShop.Controllers
{
	public class CouponsController : Controller
	{

		private readonly QShopContext _context;

		public CouponsController(QShopContext context)
		{
			_context = context;
		}
		// GET: MarketController
		public ActionResult Apply(string code)
		{
			var coupon = _context?.Coupon?.FirstOrDefault(c => c.Code == code);
			if (coupon == null)
			{
				return Ok(new
				{
					status = 202,
					message = "Không tồn tại mã giảm giá"
				});
			}
			if (DateTime.Now > coupon.ExpiryDate)
			{
				return Ok(new
				{
					status = 201,
					message = "Mã giảm giá đã hết hạn!"
				});
			}
			return Ok(new
			{
				status = 200,
				message = coupon.Description,
				percent = coupon.DiscountPercentage * 100
			});
		}
	}

}
