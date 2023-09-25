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
	public class InvoicesController : Controller
	{
		private readonly QShopContext _context;

		public InvoicesController(QShopContext context)
		{
			_context = context;
		}

		// GET: Admin/Invoicess
		public async Task<IActionResult> Index()
		{
			var invoices = _context.Invoice?.Include(i => i.user).Include(i => i.coupon);
			if (invoices != null)
			{
				return View(await invoices.ToListAsync());
			}
			return Problem("Entity set 'QShopContext.Invoice'  is null.");
		}

		public IActionResult List(int? page)
		{
			int perPage = 10;
			var invoices = _context.Invoice?.Include(i => i.user).Include(i => i.coupon).ToList();
			if (!page.HasValue)
			{
				return Json(new
				{
					items = invoices,
					count = invoices?.Count()
				});
			}
			invoices = invoices?.Skip(perPage * (page.Value - 1)).Take(perPage).ToList();
			return Json(invoices);
		}

		// GET: Admin/Accounts/Edit/5
		public async Task<IActionResult> Details(int? id)
		{
			InvoiceDetailsViewDetails invoiceDetailsViewDetails = new InvoiceDetailsViewDetails();
			if (id == null || _context.Invoice == null)
			{
				return NotFound();
			}

			var invoice = _context.Invoice.Include(i => i.user).FirstOrDefault(i => i.Id == id);
			if (invoice == null)
			{
				return NotFound();
			}

			var invoiceDetails = _context.InvoiceDetail?.Where(i => i.InvoiceId == invoice.Id).Include(i => i.account).ToList();
			if (invoiceDetails == null)
			{
				return NotFound();
			}

			var coupon = _context?.Coupon?.FirstOrDefault(c => c.Id == invoice.CouponId);
			if (coupon == null)
			{
				return NotFound();
			}

			invoiceDetailsViewDetails.InvoiceDetails = invoiceDetails;
			invoiceDetailsViewDetails.Invoice = invoice;
			invoiceDetailsViewDetails.User = invoice.user!;
			invoiceDetailsViewDetails.Coupon = invoice.coupon!;
			return View(invoiceDetailsViewDetails);
		}


		private bool InvoiceExists(int id)
		{
			return (_context.Invoice?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
