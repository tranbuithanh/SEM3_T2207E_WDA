using QShop.Models;

namespace QShop.Models
{
	public class InvoiceViewModel
	{
		public Invoice? Invoice { get; set; }
		public List<InvoiceDetail>? InvoiceDetails { get; set; }

	}
}