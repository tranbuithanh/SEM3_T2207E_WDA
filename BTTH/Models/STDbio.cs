using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTTH.Models
{
    public class STDbio
    {
        [Key]
        public int STDbioId { get; set; }

        [ForeignKey("Sbjid")]
        [Display(Name = "Subject")]
        public int  Sbjid { get; set; }
        public SubjectCls ClassroomID { get; set; }
        [ForeignKey("Stdid")]
        [Display(Name = "Student ")]
       public int  Stdid { get; set; }

         public Student StudentID { get; set; }
       

        [Required]
        public int? ExamMark { get; set; }
       // [Required]
       // public DateOnly DateStart { get; set; }
        //[Required]
       // public DateOnly DateEnd { get; set; }
        [Required]
        public int? Progress { get; set;}

        [Required, MaxLength( 2, ErrorMessage = "Choose yes or no")]
        public int? Status { get; set; }

    }
}
