using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Cooperation")]
public partial class Cooperation
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    [InverseProperty("CooperationTypeNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
