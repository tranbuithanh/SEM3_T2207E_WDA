using System.ComponentModel.DataAnnotations;

namespace LSM.Models
{
    public class Category
    {
        [Key]
        public int IdCat { get; set; }
        public string Title { get; set; }
    }
}
