using System.ComponentModel.DataAnnotations;

namespace Exam2.Models
{
    public class ProductChapter
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<ProductChapterImage> ProductChapterImages { get; set; }
    }
}
