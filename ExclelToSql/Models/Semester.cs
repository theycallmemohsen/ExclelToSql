using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Semester")]
public partial class Semester
{
    [Key]
    public int Id { get; set; }

    public int? SemesterCode { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    public bool? IsCurrent { get; set; }

    public bool? Archieved { get; set; }

    [InverseProperty("SemesterCodeNavigation")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
