using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Keyless]
public partial class Employee
{
    public int Id { get; set; }

    public int? PersonalCode { get; set; }

    [StringLength(50)]
    public string? FullName { get; set; }
}
