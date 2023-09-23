using System.Security.Claims;
using marketperry.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marketperry.Controllers;

public class AccountController : Controller
{

    private readonly applicationDbContext _context;


    public AccountController(applicationDbContext context)
    {
        _context = context;

    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string fullname, string email, string password)
    {
        var newAccount = new account
        {
            Username = fullname,
            Email = email,
            Password = password
        };

        _context.Add(newAccount);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }


    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]

    public async Task<IActionResult> Login(string email, string password)
    {
        var user = _context.accounts.FirstOrDefault(a => a.Email == email);
        if (user == null)
        {
            return Ok("Email khong ton tai");
        }

        if (user.Password != password)
        {
            return Ok("mat khau khong dung");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);


        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));
;
        return Redirect("/home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/account/login");
    }
}