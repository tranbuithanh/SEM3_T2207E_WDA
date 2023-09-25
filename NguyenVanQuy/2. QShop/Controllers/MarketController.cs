using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;
using System;
using System.Linq.Expressions;

namespace QShop.Controllers
{
	public class MarketController : Controller
	{

		private readonly QShopContext _context;

		public MarketController(QShopContext context)
		{
			_context = context;
		}
		// GET: MarketController
		public ActionResult Index(string game)
		{
			string slug = "lien-minh-huyen-thoai";
			if (!string.IsNullOrEmpty(game))
			{
				slug = game;
			}
			else
			{
				return NotFound();
			}
			ViewData["slug"] = slug;
			return View();
		}

		[HttpPost]
		public IActionResult Filter([FromBody] FilterData data)
		{
			var filterAccount = _context.Account?.Where(a => a.Status == "not sold").Include(a => a.rank).AsQueryable();
			if (filterAccount != null)
			{
				if (data.Game != null)
				{
					Game? game = _context.Game?.FirstOrDefault(g => g.Slug == data.Game);
					if (game != null)
					{
						filterAccount = filterAccount.Where(a => a.GameId == game.Id);
					}
				}
				if (data.Champion != null)
				{
					int min = Int32.Parse(data.Champion.Split('-')[0]);
					int max = Int32.Parse(data.Champion.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.Champion >= min && a.Champion <= max);
				}
				if (data.Level != null)
				{
					int min = Int32.Parse(data.Level.Split('-')[0]);
					int max = Int32.Parse(data.Level.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.Grade >= min && a.Grade <= max);
				}
				if (data.Skin != null)
				{
					int min = Int32.Parse(data.Skin.Split('-')[0]);
					int max = Int32.Parse(data.Skin.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.Skin >= min && a.Skin <= max);
				}
				if (data.Skin != null)
				{
					int min = Int32.Parse(data.Skin.Split('-')[0]);
					int max = Int32.Parse(data.Skin.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.Skin >= min && a.Skin <= max);
				}
				if (data.Rp != null)
				{
					int min = Int32.Parse(data.Rp.Split('-')[0]);
					int max = Int32.Parse(data.Rp.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.PrimaryPoint >= min && a.PrimaryPoint <= max);
				}
				if (data.Essence != null)
				{
					int min = Int32.Parse(data.Essence.Split('-')[0]);
					int max = Int32.Parse(data.Essence.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.SecondaryPoint >= min && a.SecondaryPoint <= max);
				}
				if (data.Price != null)
				{
					int min = Int32.Parse(data.Price.Split('-')[0]);
					int max = Int32.Parse(data.Price.Split('-')[1]);
					filterAccount = filterAccount.Where(a => a.Price >= min && a.Price <= max);
				}
				if (data.Rank != null && data.Rank.Count() != 0)
				{
					List<int> ranks = data.Rank;
					filterAccount = filterAccount.Where(a => ranks.Contains(a.RankId));
				}
				if (data.Order != null)
				{
					switch (data.Order)
					{
						case "newest":
							filterAccount = filterAccount.OrderByDescending(a => a.CreatedAt);
							break;
						case "oldest":
							filterAccount = filterAccount.OrderBy(a => a.CreatedAt);
							break;
						case "price-desc":
							filterAccount = filterAccount.OrderByDescending(a => a.Price);
							break;
						case "price-asc":
							filterAccount = filterAccount.OrderBy(a => a.Price);
							break;
						case "rank-desc":
							filterAccount = filterAccount.OrderByDescending(a => a.RankId);
							break;
						case "rank-asc":
							filterAccount = filterAccount.OrderBy(a => a.RankId);
							break;
					}
				}
			}
			return PartialView("_CardMarketPartial", filterAccount?.ToList());
		}

		// GET: MarketController/Details/5
		public ActionResult Details(int id)
		{


			return View();
		}

		// GET: MarketController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MarketController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MarketController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: MarketController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MarketController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: MarketController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
