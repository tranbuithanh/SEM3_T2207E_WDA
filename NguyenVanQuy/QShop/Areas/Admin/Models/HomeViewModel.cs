using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class HomeViewModel
	{
		public int AccountSoldDay { get; set; }
		public double AmountDay { get; set; }
		public int AccountSoldMonth { get; set; }
		public double AmountMonth { get; set; }

		public List<int> AccountSoldPerMonth { get; set; } = new List<int>();
		public List<double> AmountPerMonth { get; set; } = new List<double>();

	}
}