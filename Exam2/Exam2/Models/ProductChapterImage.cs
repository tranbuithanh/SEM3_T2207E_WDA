using System.ComponentModel.DataAnnotations;

namespace Exam2.Models
{
    public class ProductChapterImage
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductChapterId { get; set; }
        public ProductChapter ProductChapter { get; set; }
    }
}
