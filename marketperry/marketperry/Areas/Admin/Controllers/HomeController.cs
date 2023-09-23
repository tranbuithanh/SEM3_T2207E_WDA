using marketperry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketperry.Areas.Admin.Controllers
{
	[Area("Admin")]
	
	public class HomeController : Controller
	{
        private readonly applicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, applicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
	}
}
