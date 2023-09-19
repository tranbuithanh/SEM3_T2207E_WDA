using System;
using System.Collections.Generic;

namespace CTsinhvien.Models;

public partial class Person
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Tel { get; set; } = null!;
}
