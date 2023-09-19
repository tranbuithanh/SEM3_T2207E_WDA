using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Exam
{
    public int Exid { get; set; }

    public float Exresult { get; set; }

    public DateTime Exdate { get; set; }

    public int Excourse { get; set; }

    public int Extime { get; set; }

    public int Studentid { get; set; }

    public int Subjectclsid { get; set; }

    public int Clsroomid { get; set; }

    public virtual Classroom Clsroom { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subjectcla Subjectcls { get; set; } = null!;
}
