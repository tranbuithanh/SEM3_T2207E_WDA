using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace QShop.Controllers
{
	public class HomeController : Controller
	{
		//private readonly ILogger<HomeController> _logger;
		private readonly QShopContext _context;

		//public HomeController(ILogger<HomeController> logger)
		//{
		//    _logger = logger;
		//}


		public HomeController(QShopContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			User user = new User();
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([Bind("Email,Password")] User user)
		{
			ModelState.Remove("UserName");
			if (ModelState.IsValid)
			{
				var findUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
				bool isValidPassword = false;
				if (findUser != null)
				{
					isValidPassword = BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password);
					if (isValidPassword)
					{
						var claims = new List<Claim> {
													new Claim(ClaimTypes.Name, findUser.Email),
													new Claim(ClaimTypes.Role, findUser.Role),
													new Claim("UserId", findUser.Id.ToString()),};
						var claimsIdentity = new ClaimsIdentity(
						claims, CookieAuthenticationDefaults.AuthenticationScheme);
						await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity));
						return RedirectToAction("index");
					}
				}
				ModelState.AddModelError("Email", "*Tài khoản hoặc mật khẩu không đúng.");
			}
			return View(user);

		}


		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index");
		}

		public IActionResult Register()
		{
			User user = new User();
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([Bind("Email, Birthday, Password")] User user)
		{
			ModelState.Remove("ImageUpload");
			ModelState.Remove("UserName");
			if (ModelState.IsValid)
			{
				var findUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
				if (findUser != null)
				{
					ModelState.AddModelError("Email", "*Email đã tồn tại!");
				}
				else
				{
					user.UserName = user.Email.Split('@')[0];
					user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
					await _context.User.AddAsync(user);
					_context.SaveChanges();
					Notification noti = new Notification("register")
					{
						UserId = user.Id
					};
					_context.Add(noti);
					_context.SaveChanges();
					return RedirectToAction("Login");
				}
			}
			return View(user);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult beingCompleted()
		{
			return View();
		}


	}
}