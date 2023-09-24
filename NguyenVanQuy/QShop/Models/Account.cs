using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Account
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "*Email không được để trống.")]
		public string Email { get; set; } = string.Empty;
		[DisplayName("Mật khẩu")]
		public string Password { get; set; } = "1";
		[DisplayName("Riot point"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public int PrimaryPoint { get; set; } = 0;
		[DisplayName("Tinh hoa lam"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public int SecondaryPoint { get; set; } = 0;

		[DisplayName("Tướng")]
		public int Champion { get; set; } = 0;
		public int Skin { get; set; } = 0;

		[DisplayName("Cấp")]
		public int Grade { get; set; } = 30;
		[DisplayName("Giá"), DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
		public int Price { get; set; } = 10000;

		[DisplayName("Ngày tạo"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public string Status { get; set; } = "not sold";
		public string Description { get; set; } = "";

		// [ForeignKey("Game")]
		[DisplayName("Game")]
		[Required(ErrorMessage = "*Game không được để trống.")]
		public int GameId { get; set; }
		[Required(ErrorMessage = "*Game không được để trống.")]
		public virtual Game? game { get; set; }

		[ForeignKey("Rank")]
		[DisplayName("Rank")]
		[Required(ErrorMessage = "*Rank không được để trống.")]
		public int RankId { get; set; }
		[Required(ErrorMessage = "*Rank không được để trống.")]
		public virtual Rank? rank { get; set; }
	}
}
