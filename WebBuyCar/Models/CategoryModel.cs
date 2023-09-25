using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBuyCar.Models
{
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }
		[Required, MinLength(4,ErrorMessage = "Requires entering a category name")]
		public string NameCategory { get; set; }

        [Required, MinLength(4, ErrorMessage = "Requires entering a category Description")]
        public string Description { get; set; }

		public string ImageCategory { get; set; }

		public string Slug { get; set; }

		public int Status { get; set; }

        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpload { get; set; }

    }
}

