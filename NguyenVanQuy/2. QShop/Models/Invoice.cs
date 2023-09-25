using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Invoice
	{
		[Key]
		public int Id { get; set; }

		[DisplayName("Ngày mua"), DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime InvoiceDate { get; set; } = DateTime.Now;

		[DisplayName("Tổng tiền"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public double Amount { get; set; } = 0;
		[DisplayName("Tổng tiền thanh toán"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public double TotalAmount { get; set; } = 0;

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User? user { get; set; }

		[ForeignKey("Coupon")]
		public int CouponId { get; set; } = 0;
		public virtual Coupon? coupon { get; set; }

		public Invoice()
		{
			this.InvoiceDate = DateTime.Now;
			this.Amount = 0;
			this.TotalAmount = 0;
			this.UserId = 0;
			this.CouponId = 1;
		}
	}

}