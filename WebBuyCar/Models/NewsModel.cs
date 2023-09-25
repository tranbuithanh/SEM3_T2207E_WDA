using System;
using System.ComponentModel.DataAnnotations;

namespace WebBuyCar.Models
{
	public class NewsModel
	{
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Requires entering a brand name")]
        public string TitleNews { get; set; }

        [Required( ErrorMessage = "Requires entering time")]
        public DateTime timeUpNews { get; set; }

        [Required, MinLength(4, ErrorMessage = "Requires entering a description name")]
        public string DesNews { get; set; }

        public string ImageNews { get; set; }
        public string Slug { get; set; }
        public int arrangeNews { get; set; }
        public int Status { get; set; } 
    }
}

