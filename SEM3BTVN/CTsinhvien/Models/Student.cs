using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Student
{
    public int Studentid { get; set; }

    public string Studentname { get; set; } = null!;

    public string Studenttel { get; set; } = null!;

    public DateTime Studentbirth { get; set; }

    public string Studentadress { get; set; } = null!;

    public int Classid { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
