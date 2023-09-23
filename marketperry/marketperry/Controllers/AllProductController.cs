using System;
using marketperry.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketperry.Controllers
{
	public class AllProductController :Controller
	{
        private readonly applicationDbContext _context;
        public AllProductController(applicationDbContext context)
        {
            _context = context;

        }
        public IActionResult AppleWatch()
        {
	        var appleWatch = _context.applewatchs.ToList();
			return View(appleWatch);
		}
        public IActionResult iphone()
        {
	        var mobilePhones = _context.mobilephones.ToList();
	        return View(mobilePhones);
        }
	}
}

