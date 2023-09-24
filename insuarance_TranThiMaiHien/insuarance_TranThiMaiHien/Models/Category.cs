using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuarance_TranThiMaiHien.Models
{
    [Table("categories")]
    public class Category
	{
        
        
            [Key]
            public int id { get; set; }
            [Required(ErrorMessage = "Please enter a category name")]
            [MinLength(6, ErrorMessage = "Please enter a minimum of 6 characters")]
            [Display(Name = "Category Name")]
            public string name { get; set; }

            public virtual ICollection<Product> products { get; set; }
        
    }
}

