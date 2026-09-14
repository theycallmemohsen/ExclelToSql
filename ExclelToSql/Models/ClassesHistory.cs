using System.ComponentModel.DataAnnotations;

namespace ExclelToSql.Models
{
    public class ClassesHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int? CourseId { get; set; }
        public string? TeacherId { get; set; }
        public int? Section { get; set; }
        public int? StudentsCount { get; set; }
        public int? SessionsCount { get; set; }
        public DateOnly? CreationDate { get; set; }
        public int UpdateFlag { get; set; }






    }
}
