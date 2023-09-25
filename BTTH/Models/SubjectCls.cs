using System.ComponentModel.DataAnnotations;

namespace BTTH.Models
{
    public class SubjectCls
    {
        [Key]
        public int Sbjid { get; set; }
        [Required, MinLength(3, ErrorMessage = "Required to enter  Subject name (minlength = 3).")]
        public string? SbjName { get; set; }
        [Required]
        public string? SbjDescription { get; set; }
        [Required]
        public int? SbjOrder { get; set; }
        [Required]
        public int? SbjCourse { get;}

        public virtual ICollection<STDbio>? STDbio { get; set; }

      
    }
}
