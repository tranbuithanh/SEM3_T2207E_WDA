using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class Cart
	{

		[Key]
		public int Id { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User? user { get; set; }
		[ForeignKey("Account")]
		public int AccountId { get; set; }
		public virtual Account? account { get; set; }
	}
}
