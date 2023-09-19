using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Subjectcla
{
    public int Subjectclsid { get; set; }

    public string Subjectname { get; set; } = null!;

    public int Course { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
