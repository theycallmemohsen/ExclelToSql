using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("CoursesHistory")]
public partial class CoursesHistory
{
    [Key]
    public int HistoryId { get; set; }

    public int? CourseId { get; set; }

    [StringLength(100)]
    public string? CourseName { get; set; }

    public double? Theoretical { get; set; }

    public double? Practical { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CreditsCoefficients { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int? UpdateFlag { get; set; }

    [StringLength(50)]
    public string? EditorName { get; set; }
}
