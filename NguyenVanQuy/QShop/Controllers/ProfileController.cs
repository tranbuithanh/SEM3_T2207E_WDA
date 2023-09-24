using Microsoft.AspNetCore.Mvc;
using QShop.Data;
using QShop.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace QShop.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly QShopContext _context;

		public ProfileController(QShopContext context)
		{
			_context = context;
		}

		public ActionResult Index()
		{
			int userId = 0;
			if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
			{
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				return View(user);
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("Id, UserName,Email,Birthday, ImageUpload")] User newUser)
		{
			ModelState.Remove("Password");
			if (ModelState.IsValid)
			{
				string uploadedFileName = string.Empty;
				var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars");
				if (newUser.ImageUpload != null)
				{
					uploadedFileName = await Helper.UploadImageAsync(newUser.ImageUpload, uploadPath);
				}
				string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
				int userId = Int32.Parse(userIdClaimValue);
				var user = _context.User.FirstOrDefault(u => u.Id == userId);
				// Check Email
				if (user != null)
				{
					user.UserName = newUser.UserName;
					var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == newUser.Email && u.Email != user.Email);
					if (existingUser != null)
					{
						ModelState.AddModelError("Email", "*Email đã tồn tại!");
						return View("Index", newUser);
					}
					user.Email = newUser.Email;
					user.Birthday = newUser.Birthday;
					_context.Update(user);
					if (uploadedFileName != "")
					{
						if (user.Thumbnail != "/images/avatars/avatar-default.jpg")
						{
							Helper.DeleteImageAsync(user.Thumbnail);
						}
						user.Thumbnail = Path.Combine("/images/avatars", uploadedFileName);
					}
					await _context.SaveChangesAsync();
					return RedirectToAction("Index", "Profile");
				}
				else
				{
					return Problem("Xác thực không thành công!");
				}
			}
			else
			{
				return View("Index", newUser);
			}
		}


		[HttpPost]
		public async Task<IActionResult> ChangePassword([FromBody] Password password)
		{
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			var user = _context.User.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				if (BCrypt.Net.BCrypt.Verify(password.PasswordOld, user.Password))
				{
					user.Password = BCrypt.Net.BCrypt.HashPassword(password.PasswordNew);
					_context.Update(user);
					Notification noti = new Notification("changePassword")
					{
						UserId = userId
					};
					_context.Add(noti);
					await _context.SaveChangesAsync();
					return Ok(new { status = 200, message = "Đổi mật khẩu thành công." });
				}
				else
				{
					return Ok(new { status = 500, message = "Mật khẩu không đúng." });
				}
			}
			return Ok(new { status = 500, message = "Lỗi xác thực tài khoản!" });
		}

		public ActionResult Transaction()
		{
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			// var user = _context.User.FirstOrDefault(u => u.Id == userId);
			var invoices = _context.Invoice?.Include(i => i.coupon).Where(i => i.UserId == userId).OrderByDescending(i => i.InvoiceDate).ToList();
			List<InvoiceViewModel> list = new List<InvoiceViewModel>();
			foreach (var invoice in invoices)
			{
				InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
				invoiceViewModel.Invoice = invoice;
				var invoiceDetails = _context.InvoiceDetail.Include(i => i.account).Where(i => i.InvoiceId == invoice.Id).ToList();
				invoiceViewModel.InvoiceDetails = invoiceDetails;
				list.Add(invoiceViewModel);
			}
			return View(list);
		}

		[HttpPost]
		public IActionResult RechargeCard([FromBody] PhoneCard card)
		{
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			var user = _context.User.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				user.Balance += card.Denomination;
				Notification noti = new Notification("rechargeCard", card.Denomination)
				{
					UserId = userId
				};
				_context.Add(noti);
				_context.SaveChangesAsync();
				return Ok("Ok");
			}
			return Ok("User not found!");
		}


		public IActionResult RechargeBank()
		{
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			var user = _context.User.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				user.Balance += 500000;
				Notification noti = new Notification("rechargeBank", 500000)
				{
					UserId = userId
				};
				_context.Add(noti);
				_context.SaveChangesAsync();
				return Ok("Ok");
			}
			return Ok("User not found!");
		}

		public ActionResult Checkout(string code)
		{
			var coupon = _context?.Coupon?.FirstOrDefault(c => c.Code == code);
			if (string.IsNullOrEmpty(code))
			{
				coupon = _context?.Coupon?.FirstOrDefault(c => c.Code == "DEFAULT");
			}
			string userIdClaimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
			int userId = Int32.Parse(userIdClaimValue);
			var user = _context?.User.FirstOrDefault(u => u.Id == userId);
			var carts = _context?.Cart?.Where(c => c.UserId == userId).Include(c => c.account).ToList();
			if (carts == null)
			{
				return BadRequest();
			}
			// Chuyển account thành đã bán
			var accountIds = carts?.Select(cart => cart?.account?.Id).ToList();
			_context?.Account?.Where(a => accountIds.Contains(a.Id)).ToList().ForEach(account =>
			{
				account.Status = "sold";
			});
			Invoice invoice = new Invoice();
			invoice.UserId = userId;
			invoice.InvoiceDate = DateTime.Now;
			_context?.Invoice?.Add(invoice);
			_context?.SaveChanges();
			foreach (var cart in carts)
			{
				invoice.Amount += cart?.account?.Price ?? 0;
				InvoiceDetail invoiceDetail = new InvoiceDetail();
				invoiceDetail.Subtotal = cart?.account?.Price ?? 0;
				invoiceDetail.AccountId = cart?.account?.Id ?? 0;
				invoiceDetail.InvoiceId = invoice.Id;
				_context?.InvoiceDetail?.Add(invoiceDetail);
			}
			invoice.TotalAmount = Math.Round(invoice.Amount * (1 - (coupon?.DiscountPercentage ?? 0)));
			user.Balance = (int)Math.Round(user.Balance - invoice.TotalAmount);
			invoice.CouponId = coupon.Id;
			_context?.Invoice?.Update(invoice);
			if (carts.Any())
			{
				_context?.Cart?.RemoveRange(carts);
			}
			Notification noti = new Notification("checkout")
			{
				UserId = userId
			};
			_context?.Notification?.Add(noti);
			_context?.SaveChanges();
			return Ok("Ok");
		}
	}
}
