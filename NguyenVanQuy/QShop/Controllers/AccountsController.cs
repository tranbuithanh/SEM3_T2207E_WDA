using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;
using QShop.Models.ViewModels;

namespace QShop.Controllers
{
	public class AccountsController : Controller
	{
		public int pageSize = 3;
		private readonly QShopContext _context;

		public AccountsController(QShopContext context)
		{
			_context = context;
		}

		// GET: Accounts/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Account == null)
			{
				return NotFound();
			}

			var account = await _context.Account
					.Include(a => a.game)
					.Include(a => a.rank)
					.FirstOrDefaultAsync(m => m.Id == id);
			if (account == null)
			{
				return NotFound();
			}

			return View(account);
		}
	}
}
