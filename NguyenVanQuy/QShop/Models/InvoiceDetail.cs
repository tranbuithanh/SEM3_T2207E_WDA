using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class InvoiceDetail
	{
		[Key]
		public int Id { get; set; }
		public int Subtotal { get; set; }

		[ForeignKey("Account")]
		public int AccountId { get; set; }
		public virtual Account? account { get; set; }

		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }
		public virtual Invoice? invoice { get; set; }
	}
}