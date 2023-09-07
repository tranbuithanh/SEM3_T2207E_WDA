using System;
using System.ComponentModel.DataAnnotations;

namespace DemoWebMVC2.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string CustName { get; set; }

		[MaxLength(100)]
		public string CustAdress { get; set; }
		
	}
}

