using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Classcourse
{
    public int Subjectclsid { get; set; }

    public int Clsroomid { get; set; }

    public DateTime Coursedate { get; set; }

    public int Coursetime { get; set; }

    public virtual Classroom Clsroom { get; set; } = null!;

    public virtual Subjectcla Subjectcls { get; set; } = null!;
}
