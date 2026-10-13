using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Models;

public partial class Teacher
{
    public int Id { get; set; }

    [Key]
    [StringLength(50)]
    public string NationalCode { get; set; } = null!;

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

    public int? UpdateFlag { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int? Duty { get; set; }

    public int? GroupCode { get; set; }

    public int? SemesterCode { get; set; }

    public int? CooperationType { get; set; }

    [StringLength(50)]
    public string? TempCooperationType { get; set; }

    [StringLength(50)]
    public string? TempGroupCode { get; set; }

    [StringLength(50)]
    public string? TempRanking { get; set; }

    [StringLength(50)]
    public string? TempDuty { get; set; }

    [InverseProperty("NationalCodeNavigation")]
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    [ForeignKey("CooperationType")]
    [InverseProperty("Teachers")]
    public virtual Cooperation? CooperationTypeNavigation { get; set; }

    [ForeignKey("Duty")]
    [InverseProperty("Teachers")]
    public virtual Duty? DutyNavigation { get; set; }

    [ForeignKey("Grade")]
    [InverseProperty("Teachers")]
    public virtual Fee? GradeNavigation { get; set; }

    [ForeignKey("GroupCode")]
    [InverseProperty("Teachers")]
    public virtual EducationalGroup? GroupCodeNavigation { get; set; }

    [ForeignKey("Ranking")]
    [InverseProperty("Teachers")]
    public virtual Ranking? RankingNavigation { get; set; }
}
