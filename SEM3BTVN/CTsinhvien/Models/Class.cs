using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Classname { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
