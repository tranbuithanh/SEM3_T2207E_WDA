using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuarance_TranThiMaiHien.Models
{
    [Table("products")]
    public class Product
	{
        [Key]
        public int id { get; set; }

        
        [StringLength(200)]
        [Required(ErrorMessage = "Please enter a category name")]
        [MinLength(6, ErrorMessage = "Please enter a minimum of 6 characters")]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Sale Price")]
        public decimal sale_price { get; set; }
        [Display(Name = "Image")]
        public string img { get; set; }
        [Display(Name = "Description")]
        [Column(TypeName = "text")]
        public string description { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        [Display(Name = "Qty")]
        public int qty { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Created at")]
        public DateTime created_at { get; set; } = DateTime.Now;

        [ForeignKey("category")]
        public int category_id { get; set; }
        public virtual Category category { get; set; }
    }
}

