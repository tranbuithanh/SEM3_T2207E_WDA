using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC4.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string CustName { get; set; } = "";
		public string CustAdress { get; set; } = "";


    }
}

