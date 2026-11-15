using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Major
{
    [Key]
    public int Id { get; set; }

    public int? FacultyId { get; set; }

    public int? GroupId { get; set; }

    [StringLength(50)]
    public string? Faculty { get; set; }

    [StringLength(50)]
    public string? EducationalGroup { get; set; }

    [Column("Major")]
    [StringLength(100)]
    public string? Major1 { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("Majors")]
    public virtual Faculty? FacultyNavigation { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Majors")]
    public virtual EducationalGroup? Group { get; set; }
}
