using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly QShopContext _context;

		public UsersController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Users
		public async Task<IActionResult> Index()
		{
			return View();
		}

		public IActionResult List(int? page)
		{
			int perPage = 10;
			var users = _context.User?.ToList();
			if (!page.HasValue)
			{
				return Json(new
				{
					items = users,
					count = users?.Count()
				});
			}
			users = users?.Skip(perPage * (page.Value - 1)).Take(perPage).ToList();
			return Json(users);
		}


		// GET: Admin/Users/Create
		public IActionResult Create()
		{
			User user = new User();
			return View(user);
		}

		// POST: Admin/Users/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(User _user)
		{
			ModelState.Remove("ImageUpload");
			var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");
			if (ModelState.IsValid)
			{
				if (_user.ImageUpload != null)
				{
					string uploadedFileName = await Helper.UploadImageAsync(_user.ImageUpload, uploadPath);
					_user.Thumbnail = Path.Combine("/images/avatars", uploadedFileName);
				}
				_context.Add(_user);
				await _context.SaveChangesAsync();
				return Redirect("/admin/users");
			}
			return View(_user);
		}

		// GET: Admin/Users/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.User == null)
			{
				return NotFound();
			}

			var user = await _context.User.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// POST: Admin/Users/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, User user)
		{
			if (Id != user.Id)
			{
				return NotFound();
			}
			ModelState.Remove("ImageUpload");
			if (ModelState.IsValid)
			{
				var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");
				string uploadedFileName = await Helper.UploadImageAsync(user.ImageUpload, uploadPath);
				if (uploadedFileName != "")
				{
					if (user.Thumbnail != "/images/avatars/avatar-default.jpg")
					{
						Helper.DeleteImageAsync(user.Thumbnail);
					}
					user.Thumbnail = Path.Combine("/images/avatars", uploadedFileName);

				}
				_context.User.Update(user);
				_context.SaveChanges();
				return Redirect("/admin/users");
			}
			return View(user);
		}

		// GET: Admin/Accounts/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.User == null)
			{
				return NotFound();
			}
			var user = _context.User?.FirstOrDefault(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}
			Helper.DeleteImageAsync(user.Thumbnail);
			_context.Remove(user);
			_context.SaveChanges();
			return Ok("200");
		}

		private bool UserExists(int id)
		{
			return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
