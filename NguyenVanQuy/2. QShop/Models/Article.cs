using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Article
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "*Tiêu đề không được để trống.")]
		public string Title { get; set; } = string.Empty;
		[Required(ErrorMessage = "*Nội dung không được để trống.")]
		public string Description { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;

		[DisplayName("Ngày viết"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public string Thumbnail { get; set; } = "/images/article-thumbnail-default.jpg";

		[ForeignKey("User")]
		public int UserId { get; set; } = 1;
		public virtual User? user { get; set; }
		[NotMapped]
		public IFormFile? ImageUpload { get; set; }
	}
}
