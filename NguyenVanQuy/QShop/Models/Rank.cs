using System.ComponentModel.DataAnnotations;

namespace QShop.Models
{
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
    }
}
