using System;
using System.ComponentModel.DataAnnotations;

namespace Sem3.StudentManage.Models
{
	public class StudentModel
	{
		[Key]
		[Display (Name = "Mã sinh viên")]
		public string StudentId { get; set; }
		[Display(Name = "Tên Sinh viên")]
		public string StudentName { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
		[Display(Name = "Ảnh đại diện")]
		public string? Image { get; set; }
	}
}

