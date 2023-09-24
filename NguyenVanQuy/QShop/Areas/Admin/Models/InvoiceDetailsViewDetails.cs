using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class InvoiceDetailsViewDetails
	{
		public IEnumerable<InvoiceDetail> InvoiceDetails;
		public Invoice Invoice;
		public User User;
		public Coupon Coupon;
	}
}