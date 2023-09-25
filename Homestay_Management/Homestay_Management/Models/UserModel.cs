using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
	public class UserModel
	{
		[Key]
		public int UserId { get; set; }
		[Required]
		public string UserName { get; set; }


		[Required,MinLength(8,ErrorMessage = "Password must be at least 8 characters.")]
		public string Password { get; set; }


		public int IsGroup { get; set; }   
		//Đây là phân quyền tk: 0 là Admin, 1 là User nhưng chưa dùng tới
	}
}
