using System.ComponentModel.DataAnnotations;

namespace Exam2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int NumberChapter { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductChapter> ProductChapters { get; set;}
    }
}
