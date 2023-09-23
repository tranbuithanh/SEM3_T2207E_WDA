using marketperry.Areas.Admin;
using marketperry.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketperry.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AccountController : Controller
    {
        private readonly applicationDbContext _context;

        public AccountController(applicationDbContext context)
        {
            _context = context;
        }

        public IActionResult LoginAdmin()
        {
            return View();
        }
    }
}

