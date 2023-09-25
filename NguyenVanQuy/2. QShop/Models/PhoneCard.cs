
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QShop.Models
{
	public class PhoneCard
	{
		public string Type { get; set; } = string.Empty;
		[DisplayName("Thẻ"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public int Denomination { get; set; }
		public string Serial { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
	}
}
