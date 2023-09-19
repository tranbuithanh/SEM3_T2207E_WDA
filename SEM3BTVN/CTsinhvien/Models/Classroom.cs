using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Classroom
{
    public int Clsroomid { get; set; }

    public string Clsroomname { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
