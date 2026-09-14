using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Duty")]
public partial class Duty
{
    [Key]
    public int Id { get; set; }

    public string? Ranking { get; set; }

    public int? FullTimeFacultyDuty { get; set; }

    public int? DeanDeputyDirectorGeneral { get; set; }

    public int? DepartmentManager { get; set; }

    public int? DeputyandUnitHead { get; set; }

    [InverseProperty("DutyNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
