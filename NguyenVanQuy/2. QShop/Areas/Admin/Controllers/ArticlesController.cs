using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ArticlesController : Controller
	{
		private readonly QShopContext _context;

		public ArticlesController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Articles
		public async Task<IActionResult> Index()
		{
			var articles = _context?.Article?.Include(a => a.user);
			if (articles != null)
			{
				return View(await articles.ToListAsync());
			}
			return Problem("Entity set 'QShopContext.Articles'  is null.");
		}

		public IActionResult List(int? page)
		{
			int perPage = 9;
			var articles = _context.Article?.Include(i => i.user).ToList();
			if (!page.HasValue)
			{
				return Json(new
				{
					items = articles,
					count = articles?.Count()
				});
			}
			articles = articles?.Skip(perPage * (page.Value - 1)).Take(perPage).ToList();
			return Json(articles);
		}

		// GET: Admin/Articles/Create
		public IActionResult Create()
		{
			Article article = new Article();
			return View(article);
		}

		// POST: Admin/Articles/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Article _article)
		{
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			_article.UserId = userId;
			ModelState.Remove("ImageUpload");
			if (ModelState.IsValid)
			{
				if (_article.ImageUpload != null)
				{
					using (var memoryStream = new MemoryStream())
					{
						await _article.ImageUpload.CopyToAsync(memoryStream);
						byte[] bytes = memoryStream.ToArray();
						_article.Thumbnail = "data:image/webp;base64," + Convert.ToBase64String(bytes);
					}
				}
				_context?.Article?.Add(_article);
				_context?.SaveChanges();
				return Redirect("/admin/articles");
			}
			return View(_article);
		}

		// GET: Admin/Articles/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Article == null)
			{
				return NotFound();
			}

			var article = await _context.Article.FindAsync(id);
			if (article == null)
			{
				return NotFound();
			}
			return View(article);
		}

		// POST: Admin/Articles/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Article _article)
		{
			if (id != _article.Id)
			{
				return NotFound();
			}
			ModelState.Remove("ImageUpload");
			if (ModelState.IsValid)
			{
				if (_article.ImageUpload != null)
				{
					using (var memoryStream = new MemoryStream())
					{
						await _article.ImageUpload.CopyToAsync(memoryStream);
						byte[] bytes = memoryStream.ToArray();
						_article.Thumbnail = "data:image/webp;base64," + Convert.ToBase64String(bytes);
					}
				}
				_context?.Article?.Update(_article);
				_context?.SaveChanges();
				return Redirect("/admin/articles");
			}
			return View(_article);

		}

		// GET: Admin/Articles/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null || _context.Article == null)
			{
				return NotFound();
			}
			var article = _context.Article?.FirstOrDefault(m => m.Id == id);
			if (article == null)
			{
				return NotFound();
			}
			_context.Remove(article);
			_context.SaveChanges();
			return Ok("200");
		}

		private bool ArticleExists(int id)
		{
			return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
