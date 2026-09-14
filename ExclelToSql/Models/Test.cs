using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Test")]
public partial class Test
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string? PassWord1 { get; set; }

    [StringLength(10)]
    public string Auth { get; set; } = null!;

    [ForeignKey("Auth")]
    [InverseProperty("Tests")]
    public virtual UserLevel AuthNavigation { get; set; } = null!;
}
