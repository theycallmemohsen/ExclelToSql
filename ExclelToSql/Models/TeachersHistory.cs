using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("TeachersHistory")]
public partial class TeachersHistory
{
    [Key]
    public int HistoryId { get; set; }

    [StringLength(50)]
    public string PersonalCode { get; set; } = null!;

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    public int? Ranking { get; set; }

    public int? Grade { get; set; }

    [StringLength(50)]
    public string? AccountNumber { get; set; }

    [StringLength(50)]
    public string? InsuranceNumber { get; set; }

    [StringLength(50)]
    public string? NationalCode { get; set; }

    public int UpdateFlag { get; set; }

    public DateOnly CreationDate { get; set; }

    [StringLength(50)]
    public string? EditorName { get; set; }
}
