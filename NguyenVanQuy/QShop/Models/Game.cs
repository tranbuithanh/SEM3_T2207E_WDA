using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Game
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "*Tên game không được để trống.")]
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Card { get; set; } = string.Empty;
		public string CardText { get; set; } = string.Empty;
		public string Logo { get; set; } = string.Empty;
		[Required(ErrorMessage = "*Slug không được để trống.")]
		public string Slug { get; set; } = string.Empty;

		// [InverseProperty("game")]
		public virtual ICollection<Account> accounts { get; set; }
		[NotMapped]
		[Required(ErrorMessage = "*Bạn chưa chọn ảnh.")]
		public IFormFile? ImageUploadCard { get; set; }
		[NotMapped]
		[Required(ErrorMessage = "*Bạn chưa chọn ảnh.")]
		public IFormFile? ImageUploadCardText { get; set; }
		[NotMapped]
		[Required(ErrorMessage = "*Bạn chưa chọn ảnh.")]
		public IFormFile? ImageUploadLogo { get; set; }
	}
}
