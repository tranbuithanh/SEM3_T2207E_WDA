using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QShop.Data;
using QShop.Models;

namespace QShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ResetController : Controller
	{
		private readonly QShopContext _context;
		public ResetController(QShopContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Yes()
		{
			var notifications = _context.Notification?.ToList();
			if (notifications != null)
			{
				_context.Notification?.RemoveRange(notifications);
			}
			var invoices = _context.Invoice?.ToList();
			if (invoices != null)
			{
				_context.Invoice?.RemoveRange(invoices);
			}
			var invoiceDetails = _context.InvoiceDetail?.ToList();
			if (invoiceDetails != null)
			{
				_context.InvoiceDetail?.RemoveRange(invoiceDetails);
			}
			var accounts = _context.Account?.Where(a => a.Status == "sold").ToList();
			if (accounts != null)
			{
				foreach (var item in accounts)
				{
					item.Status = "not sold";
				}
			}
			_context.SaveChanges();
			return Redirect("/admin");
		}
	}
}
