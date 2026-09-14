using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("Archive")]
public partial class Archive
{
    [Key]
    public int Id { get; set; }

    public int EditorId { get; set; }
}
