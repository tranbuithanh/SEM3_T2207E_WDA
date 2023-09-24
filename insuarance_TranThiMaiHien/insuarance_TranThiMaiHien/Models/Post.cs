using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuarance_TranThiMaiHien.Models
{
    [Table("posts")]
    public class Post
    {
        [Key]
        public int id { get; set; }
        [Required]

        public string img { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string boby { get; set; }
        [Required]
        public string status { get; set; }

        [ForeignKey("users")]
        public int user_id { get; set; }
        public User user { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
    }
}

