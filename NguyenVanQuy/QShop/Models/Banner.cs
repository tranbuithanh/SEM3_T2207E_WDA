using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Banner
	{

		[Key]
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
	}
}
