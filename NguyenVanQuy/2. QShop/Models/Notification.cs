using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QShop.Models
{
	public class Notification
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string Status { get; set; } = "unread";

		[DisplayName("Thời gian"), DisplayFormat(DataFormatString = "{0:hh:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[ForeignKey("To")]
		public int UserId { get; set; }
		public virtual User? to { get; set; }

		public int From { get; set; } = 1;

		public Notification()
		{
		}
		public Notification(string param, int value = 0)
		{
			switch (param)
			{
				case "checkout":
					this.Title = "🎉Giao dịch thành công🎉";
					this.Content = "Bạn đã mua thành công tài khoản. Hãy đăng nhập vào game và trải nghiệm ngay!";
					break;
				case "register":
					this.Title = "☕Đăng ký thành công☕";
					this.Content = "Chúc mừng bạn đã đăng ký tài khoản thành công trên QShop. Hãy trải nghiệm những điều thú vị của QShop ngay bây giờ!";
					break;
				case "changePassword":
					this.Title = "🔑Đổi mật khẩu thành công🔑";
					this.Content = "Bạn đã thay đổi mật khẩu thành công trên QShop. Hãy đăng xuất và đăng nhập lại để đảm bảo bảo mật tài khoản!";
					break;
				case "rechargeCard":
					this.Title = "💳Nạp thẻ thành công💳";
					this.Content = $"Bạn đã nạp thành công thẻ {value.ToString("N0")}vnđ. Hãy sử dụng số tiền trong tài khoản của bạn để mua các sản phẩm bạn muốn!";
					break;
				case "rechargeBank":
					this.Title = "💸Chuyển tiền thành công💸";
					this.Content = $"Chuyển tiền thành công + {value.ToString("N0")}vnđ. Hãy sử dụng số tiền này để mua các sản phẩm bạn muốn!";
					break;
				default:
					this.Title = "🎉Welcome to QShop🎉";
					this.Content = "Chào mừng bạn đến với QShop. Hãy khám phá các sản phẩm tuyệt vời và sử dụng số tiền trong tài khoản để mua những gì bạn muốn!";
					break;
			}
		}
	}
}
