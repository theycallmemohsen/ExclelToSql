using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("NewFee")]
public partial class NewFee
{
    [Key]
    public int Id { get; set; }

    [Column("Assistant(Master's degree)")]
    public int? AssistantMasterSDegree { get; set; }

    [Column("Instructor (equivalent to a PhD)")]
    public int? InstructorEquivalentToAPhD { get; set; }

    [Column("Assistant Professor")]
    public int? AssistantProfessor { get; set; }

    [Column("Associate Professor")]
    public int? AssociateProfessor { get; set; }

    public int? Professor { get; set; }

    [InverseProperty("Grade1")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
