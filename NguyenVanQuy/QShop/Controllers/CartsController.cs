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
	public class CartsController : Controller
	{

		private readonly QShopContext _context;

		public CartsController(QShopContext context)
		{
			_context = context;
		}
		// GET: MarketController
		public ActionResult Index()
		{
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				int totalAmount = 0;
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				int userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				var carts = _context.Cart?
					.Include(c => c.account)
						.ThenInclude(a => a.rank)
					.Include(c => c.account)
						.ThenInclude(a => a.game)
					.Where(c => c.UserId == userId)
					.ToList();
				if (carts != null)
				{
					foreach (var cart in carts)
					{
						totalAmount += cart?.account?.Price ?? 0;
					}
				}
				ViewData["amount"] = totalAmount;
				ViewData["your-money"] = user?.Balance;
				return View(carts);
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}
		}

		// GET: Cart Count
		public ActionResult Count()
		{
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				int userId = Int32.Parse(userIdClaimValue);
				var carts = _context.Cart?
										.Include(c => c.account)
										.ThenInclude(a => a.rank)
										.Where(c => c.UserId == userId)
										.ToList();
				return Ok(carts?.Count());
			}
			else
			{
				return Ok(0);
			}
		}

		// GET: MarketController/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			if (_context.Cart == null)
			{
				return Ok("error");
			}
			var cart = await _context.Cart.FindAsync(id);
			if (cart != null)
			{
				_context.Cart.Remove(cart);
			}
			await _context.SaveChangesAsync();
			return Ok("success");
		}


		public async Task<IActionResult> Create(int account)
		{
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				int userID = Int32.Parse(userIdClaimValue);
				var cart = _context.Cart?.FirstOrDefault(c => c.AccountId == account && c.UserId == userID);
				if (cart != null)
				{
					return Ok("exists");
				}
				else
				{
					Cart newCart = new Cart();
					newCart.AccountId = account;
					newCart.UserId = userID;
					_context.Add(newCart);
					await _context.SaveChangesAsync();
					return Ok("ok");
				}
			}
			return Ok("login");

		}
	}
}
