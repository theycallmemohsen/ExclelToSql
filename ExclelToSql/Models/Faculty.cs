using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Faculty")]
public partial class Faculty
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [InverseProperty("FacultyNavigation")]
    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();
}
