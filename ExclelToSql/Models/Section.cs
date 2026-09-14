using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Section
{
    [Key]
    public int Id { get; set; }

    [Column("Section")]
    public string Section1 { get; set; } = null!;

    [InverseProperty("SectionNavigation")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
