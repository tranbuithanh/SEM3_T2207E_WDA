using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;
using System;
using System.Linq.Expressions;

namespace QShop.Controllers
{
	public class GamesController : Controller
	{

		private readonly QShopContext _context;

		public GamesController(QShopContext context)
		{
			_context = context;
		}
		// GET: MarketController

		public IActionResult GameIcon()
		{
			var game = _context.Game.ToList();
			return PartialView("_GameIconPartial", game);
		}
		public IActionResult GameCard()
		{
			var game = _context.Game.ToList();
			return PartialView("_GameCardPartial", game);
		}
	}
}
