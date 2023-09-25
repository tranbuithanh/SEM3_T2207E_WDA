using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBuyCar.Models
{
	public class BannerModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string ImageBanner { get; set; }
        [Required, MinLength(4, ErrorMessage = "Requires entering a title banner")]
        public string TitleBanner { get; set; }
        [Required, MinLength(4, ErrorMessage = "Requires entering a des banner")]
        public string desBanner { get; set; }
		public int Status { get; set; }
        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpload { get; set; }
    }
}

