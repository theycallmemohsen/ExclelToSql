using System;
using System.Collections.Generic;

namespace ExclelToSql.TempModels;

public partial class CompositeSemester
{
    public string TeacherId { get; set; } = null!;

    public int SemesterCode { get; set; }
}
