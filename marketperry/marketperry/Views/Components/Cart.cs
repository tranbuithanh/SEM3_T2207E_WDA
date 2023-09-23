using marketperry.Models;
using Microsoft.AspNetCore.Mvc;

namespace marketperry.Views.Components;

public class Cart : ViewComponent
{
    private readonly applicationDbContext _context;

    public Cart(applicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        ViewData["giohang"] = 0;

        if (User.Identity.IsAuthenticated)
        {
            var email = User.Identity?.Name;
            var userLogin = _context.accounts.FirstOrDefault(u => u.Email == email);
            var carts = _context.carts.Where(c => c.UserId == userLogin.Id).ToList();
            ViewData["giohang"] = carts.Count();
        }
       
        return View();
    }
}