using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AccountsController : Controller
	{
		private readonly QShopContext _context;

		public AccountsController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Accounts
		public async Task<IActionResult> Index()
		{
			return View();
		}

		public IActionResult List(int? page)
		{
			int perPage = 10;
			var accounts = _context.Account?.Include(a => a.rank).Include(a => a.game).ToList();
			if (!page.HasValue)
			{
				return Json(new
				{
					items = accounts,
					count = accounts?.Count()
				});
			}
			accounts = accounts?.Skip(perPage * (page.Value - 1)).Take(perPage).ToList();
			return Json(accounts);
		}


		// GET: Admin/Accounts/Create
		public IActionResult Create()
		{
			ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
			ViewData["RankId"] = new SelectList(_context.Rank, "Id", "Name");
			Account account = new Account();
			return View(account);
		}

		// POST: Admin/Accounts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Account _account)
		{
			ModelState.Remove("Password");
			ModelState.Remove("PrimaryPoint");
			ModelState.Remove("SecondaryPoint");
			ModelState.Remove("Skin");
			ModelState.Remove("Grade");
			ModelState.Remove("Champion");
			ModelState.Remove("Status");
			ModelState.Remove("Price");
			ModelState.Remove("Description");
			ModelState.Remove("Game");
			ModelState.Remove("Rank");
			if (ModelState.IsValid)
			{
				if (_account.Description == null)
				{
					_account.Description = "Acc siêu cấp VIP PRO";
				}
				_context.Add(_account);
				await _context.SaveChangesAsync();
				return Redirect("/admin/accounts");
			}
			ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
			ViewData["RankId"] = new SelectList(_context.Rank, "Id", "Name");
			return View(_account);
		}

		// GET: Admin/Accounts/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Account == null)
			{
				return NotFound();
			}

			var account = await _context.Account.FindAsync(id);
			if (account == null)
			{
				return NotFound();
			}
			ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
			ViewData["RankId"] = new SelectList(_context.Rank, "Id", "Name");
			return View(account);
		}

		// POST: Admin/Accounts/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Account account)
		{
			if (id != account.Id)
			{
				return NotFound();
			}
			ModelState.Remove("Game");
			ModelState.Remove("Rank");
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(account);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AccountExists(account.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return Redirect("/admin/accounts");
			}
			ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
			ViewData["RankId"] = new SelectList(_context.Rank, "Id", "Name");
			return View(account);
		}

		// GET: Admin/Accounts/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Account == null)
			{
				return NotFound();
			}
			var account = _context.Account?.FirstOrDefault(m => m.Id == id);
			if (account == null)
			{
				return NotFound();
			}
			_context.Remove(account);
			_context.SaveChanges();
			return Ok("200");
		}

		private bool AccountExists(int id)
		{
			return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
