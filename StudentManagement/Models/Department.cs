using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? EstablishedYear { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
