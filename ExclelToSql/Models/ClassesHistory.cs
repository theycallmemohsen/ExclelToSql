using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("ClassesHistory")]
public partial class ClassesHistory
{
    [Key]
    public int HistoryId { get; set; }

    public int? Id { get; set; }

    public int? CourseId { get; set; }

    [StringLength(50)]
    public string? TeacherId { get; set; }

    public int? Section { get; set; }

    public int? StudentsCount { get; set; }

    public int? SessionsCount { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int? UpdateFlag { get; set; }

    [StringLength(50)]
    public string? EditorName { get; set; }
}
