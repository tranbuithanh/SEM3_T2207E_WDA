using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class GamesController : Controller
	{
		private readonly QShopContext _context;

		public GamesController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Games
		public async Task<IActionResult> Index()
		{
			var games = _context.Game?.ToList(); ;
			if (games != null)
			{
				return View(games);
			}
			return Problem("Entity set 'QShopContext.Account'  is null.");
		}

		// GET: Admin/Games/Create
		public IActionResult Create()
		{
			Game game = new Game();
			return View(game);
		}

		// POST: Admin/Games/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name, Description,Slug,ImageUploadCard, ImageUploadCardText, ImageUploadLogo")] Game _game)
		{
			// ModelState.Remove("ImageUploadCard");
			// ModelState.Remove("ImageUploadCardText");
			// ModelState.Remove("ImageUploadLogo");
			var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/games");
			ModelState.Remove("Description");
			if (ModelState.IsValid)
			{
				if (_game.ImageUploadCard != null && _game.ImageUploadCardText != null && _game.ImageUploadLogo != null)
				{
					_game.Card = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadCard, uploadPath));
					_game.CardText = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadCardText, uploadPath));
					_game.Logo = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadLogo, uploadPath));
				}
				_context.Game?.Add(_game);
				_context.SaveChanges();
				return Redirect("~/admin/games");
			}
			return View(_game);
		}

		// GET: Admin/Accounts/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Game == null)
			{
				return NotFound();
			}

			var game = await _context.Game.FindAsync(id);
			if (game == null)
			{
				return NotFound();
			}
			return View(game);
		}

		// POST: Admin/Accounts/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description,Slug,ImageUploadCard, ImageUploadCardText, ImageUploadLogo")] Game _game)
		{
			var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/games");
			ModelState.Remove("Description");
			ModelState.Remove("ImageUploadCard");
			ModelState.Remove("ImageUploadCardText");
			ModelState.Remove("ImageUploadLogo");
			if (id != _game.Id)
			{
				return NotFound();
			}
			var game = _context.Game?.FirstOrDefault(g => g.Id == id);
			if (game == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				if (_game.ImageUploadCard != null)
				{
					Helper.DeleteImageAsync(game.Card);
					game.Card = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadCard, uploadPath));
				}
				if (_game.ImageUploadCardText != null)
				{
					Helper.DeleteImageAsync(game.CardText);
					game.Card = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadCardText, uploadPath));
				}
				if (_game.ImageUploadLogo != null)
				{
					Helper.DeleteImageAsync(game.Logo);
					game.Card = Path.Combine("/images/games", await Helper.UploadImageAsync(_game.ImageUploadLogo, uploadPath));
				}
				game.Name = _game.Name;
				game.Description = _game.Description;
				game.Slug = _game.Slug;
				_context.Update(game);
				await _context.SaveChangesAsync();
				return Redirect("/admin/games");
			}
			return View(_game);
		}

		// GET: Admin/Accounts/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null || _context.Game == null)
			{
				return NotFound();
			}
			var game = _context.Game.Include(g => g.accounts).FirstOrDefault(g => g.Id == id);
			if (game == null)
			{
				return NotFound();
			}
			if (game.accounts.Any())
			{
				return Ok("500");
			}
			Helper.DeleteImageAsync(game.Card);
			Helper.DeleteImageAsync(game.CardText);
			Helper.DeleteImageAsync(game.Logo);
			_context.Remove(game);
			_context.SaveChanges();
			return Ok("200");
		}

		private bool GanmeExists(int id)
		{
			return (_context.Game?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
