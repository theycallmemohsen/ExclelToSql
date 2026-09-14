using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Class
{
    [Key]
    public int Id { get; set; }

    public int? CourseId { get; set; }

    [StringLength(50)]
    public string? TeacherId { get; set; }

    public int? Section { get; set; }

    public int? StudentsCount { get; set; }

    public int? SessionsCount { get; set; }

    public DateOnly? CreationDate { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Classes")]
    public virtual Course? Course { get; set; }

    [ForeignKey("Section")]
    [InverseProperty("Classes")]
    public virtual Section? SectionNavigation { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("Classes")]
    public virtual Teacher? Teacher { get; set; }
}
