using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBuyCar.Models
{
	public class BrandModel
	{
		[Key]
		public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Requires entering a brand name")]
        public string NameBrands { get; set; }

        [Required, MinLength(4, ErrorMessage = "Requires entering a description name")]
        public string Description { get; set; }

		public string ImageBrand { get; set; }
        public string Slug { get; set; }
		public int Status { get; set; }

        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpload { get; set; }
    }
}

