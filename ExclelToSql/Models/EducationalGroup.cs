using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("EducationalGroup")]
public partial class EducationalGroup
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? GroupName { get; set; }

    [InverseProperty("GroupCodeNavigation")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    [InverseProperty("Group")]
    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();

    [InverseProperty("GroupCodeNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
