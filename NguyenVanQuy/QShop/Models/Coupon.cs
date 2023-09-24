using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Coupon
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "*Mã code không được để trống.")]
		public string Code { get; set; } = string.Empty;

		[Required(ErrorMessage = "*Giảm giá không được để trống.")]
		[Range(0, 100, ErrorMessage = "*Giảm giá từ 0 đến 100%")]
		public double DiscountPercentage { get; set; }



		[Required(ErrorMessage = "*Ngày hết hạn không được để trống.")]
		[DisplayName("Ngày hết hạn"), DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime ExpiryDate { get; set; }

		[Required(ErrorMessage = "*Số lượng giảm giá không được để trống.")]
		public int SingleUse { get; set; } = 1000;

		public string Description { get; set; } = string.Empty;
	}
}