using System.ComponentModel.DataAnnotations;

namespace Exam2.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
