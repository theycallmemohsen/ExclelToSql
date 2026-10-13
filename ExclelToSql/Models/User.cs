using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string? PassWord1 { get; set; }

    [StringLength(10)]
    public string Auth { get; set; } = null!;

    [StringLength(50)]
    public string? SurName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [ForeignKey("Auth")]
    [InverseProperty("Users")]
    public virtual UserLevel AuthNavigation { get; set; } = null!;
}
