using Microsoft.AspNetCore.Mvc;
using BTTH.Models;
using BTTH.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BTTH.Controllers
{
    public class AccountController : Controller
    {

        private readonly BTTHMVCContext _context;

        public AccountController(BTTHMVCContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User _userFromPage)
        {
          
            var _user = _context.User.Where(m=>m.UsEmail == _userFromPage.UsEmail && m.UsPassword == _userFromPage.UsPassword).FirstOrDefault();
            if(_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UsEmail),
            new Claim("FullName", _user.UsName),
            new Claim(ClaimTypes.Role, _user.UsRole),
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index","Account");
            }
            return View();
        }

        public IActionResult Logout()
        { 
            HttpContext.SignOutAsync(
       CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
