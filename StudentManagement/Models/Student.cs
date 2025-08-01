using System;
using System.Collections.Generic;

namespace StudentManagement.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public bool? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? PhotoPath { get; set; }

    public string? Hometown { get; set; }

    public string? StudentSchoolYear { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
