using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("UserLevel")]
public partial class UserLevel
{
    public int Id { get; set; }

    [Key]
    [StringLength(10)]
    public string Role1 { get; set; } = null!;

    [InverseProperty("AuthNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
