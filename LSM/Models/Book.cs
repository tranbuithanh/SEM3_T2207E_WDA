using System.ComponentModel.DataAnnotations;

namespace LSM.Models
{
    public class Book
    {
        [Key]
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        public required string Category { get; set; }


    }

}

