using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Course
{
    [Column("ID")]
    public int Id { get; set; }

    [Key]
    public int CourseId { get; set; }

    [StringLength(100)]
    public string? CourseName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Theoretical { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Practical { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CreditsCoefficients { get; set; }

    public int? SessionsNumber { get; set; }

    public DateOnly? CreationDate { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
