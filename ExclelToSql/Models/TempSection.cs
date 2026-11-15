using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("TempSection")]
public partial class TempSection
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string? CourseName { get; set; }

    [StringLength(50)]
    public string? Section { get; set; }
}
