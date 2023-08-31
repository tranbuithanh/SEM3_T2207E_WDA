using System;
using System.ComponentModel.DataAnnotations;

namespace Web3.EF.FirstCode.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		[StringLength(50)]
		public string CustName { get; set; }
		[MaxLength (100)]
		public string CustAdress { get; set; }
	}
}

