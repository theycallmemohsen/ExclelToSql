using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Information
{
    [Key]
    public int Id { get; set; }

    public int EmployeeId { get; set; }
}
