using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Column
{
    [Key]
    public int Id { get; set; }

    public string? TheoreticalAmount { get; set; }

    public string? NationalNumber { get; set; }

    public string? GrossAmount { get; set; }

    public string? PersonalCode { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Grade { get; set; }

    public string? CourseName { get; set; }

    public string? RowNumber { get; set; }

    public string? CreditsCoefficients { get; set; }

    public string? Section { get; set; }

    public string? Practical { get; set; }

    public string? Duty { get; set; }

    public string? TeachingExtra { get; set; }

    public string? Theoretical { get; set; }

    public string? Ranking { get; set; }

    public string? AccountNumber { get; set; }

    public string? InsuranceNumber { get; set; }

    public string? StudentNumber { get; set; }

    public string? CreditsCoefficientsSum { get; set; }
}
