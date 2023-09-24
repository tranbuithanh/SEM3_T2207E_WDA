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
	public class ArticlesController : Controller
	{
		public int pageSize = 3;
		private readonly QShopContext _context;

		public ArticlesController(QShopContext context)
		{
			_context = context;
		}

		// GET: Accounts
		public IActionResult Index(int id)
		{
			var article = _context.Article?.FirstOrDefault();
			if (article == null) return View(null);
			return View(article);
		}

		// GET: Articles/Details/5
		public async Task<IActionResult> Details(int? id)
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
			var articles = _context.Article.OrderBy(x => Guid.NewGuid()).Where(a => a.Id != article.Id).Take(3).ToList();
			return View(new ArticleViewModel
			{
				Article = article,
				Articles = articles
			});
		}

		//
		public IActionResult List(int? page)
		{
			int perPage = 9;
			var articles = _context.Article?.Skip(1).ToList();
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

	}
}
