using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVCforMoviePage.Models
{
    [Table("category")]
    public class CategoryModel
    {
        [Key]
        public int CategoryID{ get; set;}
        public string CategoryName { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
