using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WebMVCforMoviePage.Models
{
    [Table("movies")]
    public class MovieModel
    {
        [Key]
        public int MovieId { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImgUrl { get; set; } 
        public decimal Duration { get; set; }
        public CategoryModel Category { get; set; }
    }
    
}
