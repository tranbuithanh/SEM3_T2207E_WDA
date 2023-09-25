using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.GameCards
{
	public class GameCards : ViewComponent
	{
		private readonly QShopContext _context;
		public GameCards(QShopContext context)
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
