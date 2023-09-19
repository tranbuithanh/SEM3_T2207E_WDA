using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Subjectclass
{
    public int Subjectclsid { get; set; }

    public int Studentid { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subjectcla Subjectcls { get; set; } = null!;
}
