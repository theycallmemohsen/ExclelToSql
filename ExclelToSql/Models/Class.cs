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

    [StringLength(50)]
    public string? NationalCode { get; set; }

    public int? CourseId { get; set; }

    [StringLength(50)]
    public string? TeacherId { get; set; }

    public int? Section { get; set; }

    public int? StudentsCount { get; set; }

    public int? SessionsCount { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int? GroupCode { get; set; }

    public int? SemesterCode { get; set; }

    [StringLength(50)]
    public string? PresentationCode { get; set; }

    [StringLength(50)]
    public string? TempGroupCode { get; set; }

    [StringLength(200)]
    public string? TempCourseId { get; set; }

    [StringLength(10)]
    public string? TempSemesterCode { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Classes")]
    public virtual Course? Course { get; set; }

    [ForeignKey("NationalCode")]
    [InverseProperty("Classes")]
    public virtual Teacher? NationalCodeNavigation { get; set; }

    [ForeignKey("Section")]
    [InverseProperty("Classes")]
    public virtual Section? SectionNavigation { get; set; }

    [ForeignKey("SemesterCode")]
    [InverseProperty("Classes")]
    public virtual Semester? SemesterCodeNavigation { get; set; }
}
