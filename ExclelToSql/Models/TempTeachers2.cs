using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

[Table("TempTeachers2")]
public partial class TempTeachers2
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? TempCooperationType { get; set; }

    [StringLength(50)]
    public string? PersonalCode { get; set; }

    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? TempRanking { get; set; }

    public int? Grade { get; set; }

    [StringLength(50)]
    public string? NationalCode { get; set; }

    [StringLength(50)]
    public string? AccountNumber { get; set; }

    [StringLength(50)]
    public string? InsuranceNumber { get; set; }

    [StringLength(50)]
    public string? PhoneNumber { get; set; }
}
