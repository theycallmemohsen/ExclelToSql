using System.ComponentModel.DataAnnotations;

public class TeacherSemesterDto
{
    [Required, StringLength(50)]
    public string TeacherId { get; set; } = "";

    [Range(1, int.MaxValue)]
    public int SemesterCode { get; set; }
}