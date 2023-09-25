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
	public class HomeController : Controller
	{
		private readonly QShopContext _context;
		public HomeController(QShopContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			DateTime today = DateTime.Today;
			HomeViewModel HomeVM = new HomeViewModel();
			//Doanh thu trong ngày
			var ordersToday = _context.Invoice
				.Where(order => order.InvoiceDate >= today && order.InvoiceDate < today.AddDays(1))
				.ToList();
			foreach (var item in ordersToday)
			{
				HomeVM.AmountDay += item.TotalAmount;
			}
			//Số acc bán trong ngày
			var invoiceIds = ordersToday?.Select(i => i.Id).ToList();
			var invoiceDetails = _context.InvoiceDetail
					.Where(detail => invoiceIds.Contains(detail.InvoiceId))
					.ToList();
			HomeVM.AccountSoldDay = invoiceDetails.Count();

			//Doanh thu theo tháng
			DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			var ordersThisMonth = _context.Invoice
					.Where(order => order.InvoiceDate >= firstDayOfMonth && order.InvoiceDate <= lastDayOfMonth)
					.ToList();

			foreach (var item in ordersThisMonth)
			{
				HomeVM.AmountMonth += item.TotalAmount;
			}
			//Số acc theo tháng
			var invoiceMothIds = ordersThisMonth?.Select(i => i.Id).ToList();
			var invoiceMothDetails = _context.InvoiceDetail
					.Where(detail => invoiceMothIds.Contains(detail.InvoiceId))
					.ToList();
			HomeVM.AccountSoldMonth = invoiceMothDetails.Count();

			//Theo tháng trong năm

			List<int> monthlyCounts = new List<int>();

			// Duyệt qua từng tháng trong năm
			for (int month = 1; month <= 12; month++)
			{
				double amount = 0;
				DateTime firstDayOfMonthOfYear = new DateTime(DateTime.Now.Year, month, 1);
				DateTime lastDayOfMonthOfYear = firstDayOfMonthOfYear.AddMonths(1).AddDays(-1);
				var ordersMonth = _context.Invoice
				.Where(order => order.InvoiceDate >= firstDayOfMonthOfYear && order.InvoiceDate < lastDayOfMonthOfYear)
				.ToList();
				var invoiceMonthIds = ordersMonth?.Select(i => i.Id).ToList();
				var invoiceMonthDetails = _context.InvoiceDetail
					.Where(detail => invoiceMonthIds.Contains(detail.InvoiceId))
					.ToList();
				foreach (var item in ordersMonth)
				{
					amount += item.TotalAmount;
				}
				HomeVM.AmountPerMonth.Add(amount);
				HomeVM.AccountSoldPerMonth.Add(invoiceMonthDetails.Count());
			}
			var data01 = JsonConvert.SerializeObject(HomeVM.AmountPerMonth.ToArray());
			var data02 = JsonConvert.SerializeObject(HomeVM.AccountSoldPerMonth.ToArray());
			ViewData["data01"] = data01;
			ViewData["data02"] = data02;
			return View(HomeVM);
		}
	}
}
