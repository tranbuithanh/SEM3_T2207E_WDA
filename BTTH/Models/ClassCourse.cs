using System.ComponentModel.DataAnnotations;

namespace BTTH.Models
{
    public class ClassCourse
    {
        [Key]
        public int Clsid { get; set; }
        [Required, MinLength(3, ErrorMessage = "Required to enter  Cls name (minlength = 3).")]
        public string? ClsName { get; set; }
        [Required]
        public string? ClsDescription { get; set; }
        [Required]
        public int ? ClsOrder { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
    }
}
