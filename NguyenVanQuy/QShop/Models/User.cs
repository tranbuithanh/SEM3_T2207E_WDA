using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "*Tên tài khoản không được để trống.")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "*Email không được để trống.")]
		[EmailAddress(ErrorMessage = "*Email không đúng định dạng.")]
		[Display(Name = "Email")]
		public string Email { get; set; } = string.Empty;
		public DateTime Birthday { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "*Mật khẩu không được để trống.")]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "*Mật khẩu phải có ít nhất 6 ký tự.")]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; } = string.Empty;
		public string Thumbnail { get; set; } = "/images/avatars/avatar-default.jpg";
		public int Balance { get; set; } = 0;
		public string Role { get; set; } = "Guest";

		[NotMapped]
		public IFormFile? ImageUpload { get; set; }

	}
}
