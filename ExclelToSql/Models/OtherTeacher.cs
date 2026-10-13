using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class OtherTeacher
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? PresentationCode { get; set; }

    [StringLength(50)]
    public string? TeacherId { get; set; }

    [StringLength(50)]
    public string? TeacherFullName { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }
}
