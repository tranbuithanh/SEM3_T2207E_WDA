using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Review
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public int Rating { get; set; } = 5;
		public string Status { get; set; } = "show";

		[DisplayName("Ngày viết"), DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User? user { get; set; }

	}
}
