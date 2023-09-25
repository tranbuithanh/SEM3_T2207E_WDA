using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Controllers
{
	public class ReviewsController : Controller
	{
		private readonly QShopContext _context;

		public ReviewsController(QShopContext context)
		{
			_context = context;
		}

		// GET: Ranks
		public async Task<IActionResult> Index()
		{
			int reviewCount = 0;
			int countFive = 0;
			int countFour = 0;
			int countThree = 0;
			int countTwo = 0;
			int countOne = 0;
			if (_context.Review != null)
			{
				var reviews = await _context.Review.ToListAsync();
				reviewCount = reviews.Count();
				foreach (var item in reviews)
				{
					switch (item.Rating)
					{
						case 1:
							countOne++;
							break;
						case 2:
							countTwo++;
							break;
						case 3:
							countThree++;
							break;
						case 4:
							countFour++;
							break;
						case 5:
							countFive++;
							break;
					}

					ViewData["result"] = new
					{
						one = Math.Round(countOne * 1.0 / reviewCount * 100, 0),
						two = Math.Round(countTwo * 1.0 / reviewCount * 100, 0),
						three = Math.Round(countThree * 1.0 / reviewCount * 100, 0),
						four = Math.Round(countFour * 1.0 / reviewCount * 100, 0),
						five = Math.Round(countFive * 1.0 / reviewCount * 100, 0),
						avg = Math.Round(((countOne * 1.0 + countTwo * 2.0 + countThree * 3.0 + countFour * 4.0 + countFive * 5.0) / (double)reviewCount), 2),
						count = reviewCount,
						star = Helper.generateStarRatingHTML(Math.Round(((countOne * 1.0 + countTwo * 2.0 + countThree * 3.0 + countFour * 4.0 + countFive * 5.0) / (double)reviewCount), 2))
					};
				}
			}
			else
			{
				return Problem("Entity set 'QShopContext.Rank'  is null.");
			}
			return View();
		}

		public async Task<IActionResult> Filter(string sortBy)
		{
			var reviewViewModels = new List<ReviewViewModel>();
			if (_context.Review != null)
			{
				var reviews = await _context.Review.Include(r => r.user).ToListAsync();
				foreach (var review in reviews)
				{
					var reviewViewModel = new ReviewViewModel(review);
					reviewViewModels.Add(reviewViewModel);
				}
				if (sortBy == "newest")
					reviewViewModels = reviewViewModels.OrderByDescending(r => r.Review.CreatedAt).ToList();
				else
				{
					reviewViewModels = reviewViewModels.OrderBy(r => r.Review.CreatedAt).ToList();
				}
				return PartialView("_CardReviewPartial", reviewViewModels);
			}
			else
			{
				return Problem("Entity set 'QShopContext.Rank'  is null.");
			}
		}

		// GET: Reviews/Create
		public IActionResult Create()
		{
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				int userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				return View(user);
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}
		}

		// POST: Reviews/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public async Task<IActionResult> Create([Bind("UserId, Content, Rating")] Review review)
		{
			if (ModelState.IsValid)
			{
				_context.Add(review);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		private bool ReviewsExists(int id)
		{
			return (_context.Review?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
