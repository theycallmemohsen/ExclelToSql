using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("NewInformation")]
public partial class NewInformation
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Faculty { get; set; }

    [StringLength(50)]
    public string? EducationalGroup { get; set; }

    [StringLength(100)]
    public string? Major { get; set; }
}
