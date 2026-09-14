using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Excel
{
    [Key]
    public int Id { get; set; }

    public int? RowNumber { get; set; }

    public string? NationalNumber { get; set; }

    public string? PersonalCode { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Ranking { get; set; }

    public int? Grade { get; set; }

    public string? CourseName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Theoretical { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Practical { get; set; }

    public string? Section { get; set; }

    public int? StudentNumber { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CreditsCoefficients { get; set; }

    public double? CreditsCoefficientsSum { get; set; }

    public int? Duty { get; set; }

    public double? TeachingExtra { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TheoreticalAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GrossAmount { get; set; }

    public string? AccountNumber { get; set; }

    public string? InsuranceNumber { get; set; }
}
