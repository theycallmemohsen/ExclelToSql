using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Fee")]
public partial class Fee
{
    [Key]
    public int Grade { get; set; }

    public int? BachelorTeacher { get; set; }

    public int? MastersTeacher { get; set; }

    public int? MastersTrainer { get; set; }

    [Column("PHDTrainer")]
    public int? Phdtrainer { get; set; }

    public int? AssistantTeacher { get; set; }

    public int? AssociateTeacher { get; set; }

    public int? Teacher { get; set; }

    [InverseProperty("GradeNavigation")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
