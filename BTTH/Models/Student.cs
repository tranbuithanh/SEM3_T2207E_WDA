using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTTH.Models
{
    public class Student
    {
        [Key]
        public int Stdid { get; set; }
        [Required, MinLength(3, ErrorMessage = "Required to enter  Student name (minlength = 3).")]
        public string? StdName { get; set; }
        [Required]
        public DateTime StdBirth { get; set; }
        [Required]
        public string? StdTel { get; set; }
        [Required]
        public string? StdAdr { get; set; }
        [Required]
        public string? StdImg { get;set; }
        
        [ForeignKey("Clsid")]
        [Display(Name = "Class")]
          public int Clsid { get; set; }

        public   ClassCourse Clsroom { get; set; }

        public virtual ICollection<STDbio>? StudentBio { get; set; }

         

    }

   
    

}
