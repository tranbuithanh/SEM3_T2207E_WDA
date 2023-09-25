using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBuyCar.Repository.Validation;
using WebBuyCar.Models;

namespace WebBuyCar.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Requires entering a Product name")]
        public string NameCar { get; set; }

        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Requires entering a Description")]
        public string Description { get; set; }
        [Required (ErrorMessage = "Requires entering a Year Of Manu facture")]
        public int YearOfManufacture { get; set; }
        [Required(ErrorMessage = "Requires entering a Price")]
        public decimal Price { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public CategoryModel Category { get; set; }
		public BrandModel Brand { get; set; }
		public int status { get; set; }
		public string ImageProduct { get; set; }
		[NotMapped]
		[FileExtensions]
		public IFormFile ImageUpload { get; set; }
	}
}

