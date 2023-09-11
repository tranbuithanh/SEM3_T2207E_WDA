using System;
using System.ComponentModel.DataAnnotations;

namespace Web3.ProductManager.Models
{
	public class StoreModel
	{
		[Key]
		[Display(Name ="Store Id")]
		public string StoreId { get; set; }
		[StringLength(100)]
		[Display(Name ="Store Name")]
		public string StoreName { get; set; }
		[StringLength(250)]
		[Display(Name ="Address")]
		public string Address { get; set; }
	}
}

