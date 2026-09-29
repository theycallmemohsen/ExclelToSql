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

    [Column("DutyGroupID")]
    public int? DutyGroupId { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    public int? Value { get; set; }

    [ForeignKey("DutyGroupId")]
    [InverseProperty("Duties")]
    public virtual Ranking? DutyGroup { get; set; }

    [InverseProperty("DutyNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
