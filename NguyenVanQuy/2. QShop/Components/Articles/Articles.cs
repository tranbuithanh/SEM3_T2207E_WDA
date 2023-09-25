using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Security.Claims;

namespace QShop.Components.Articles
{
	public class Articles : ViewComponent
	{
		private readonly QShopContext _context;
		public Articles(QShopContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var articles = _context.Article?.Take(3).ToList();
			return View(articles);
		}
	}
}
