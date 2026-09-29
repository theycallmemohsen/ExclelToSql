using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Ranking
{
    [Key]
    public int Id { get; set; }

    [Column("Ranking")]
    public string Ranking1 { get; set; } = null!;

    [InverseProperty("DutyGroup")]
    public virtual ICollection<Duty> Duties { get; set; } = new List<Duty>();

    [InverseProperty("RankingNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
