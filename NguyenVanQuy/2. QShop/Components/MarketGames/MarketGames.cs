using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.MarketGames
{
	public class MarketGames : ViewComponent
	{
		private readonly QShopContext _context;
		public MarketGames(QShopContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var games = _context.Game.ToList();
			// return View("GameCard", reviews);
			return View(games);
		}
	}
}
