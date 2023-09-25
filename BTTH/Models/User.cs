using System.ComponentModel.DataAnnotations;

namespace BTTH.Models
{
    public class User
    {
        [Key]
        public int Usid { get; set; }
        [Required, MinLength(3, ErrorMessage = "Required to enter  User name (minlength = 3).")]
        public string? UsName { get; set; }
        [Required]
        public string? UsEmail { get; set; }
        [Required, MinLength(8, ErrorMessage = "Required to enter User password (minlength = 8).")]
        public string? UsPassword { get; set; }
        [Required]
        public string? UsRole { get; set;}
    }
}
