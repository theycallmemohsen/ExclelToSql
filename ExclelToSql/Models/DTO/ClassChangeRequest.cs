namespace ExclelToSql.Models.DTO
{
    public class ClassChangeRequest
    {
        public int CourseId { get; set; } 
        public string? TeacherId { get; set; } 
        public int? Section {  get; set; }  
        public int? StudentsCount {  get; set; }
        public int? SessionsCount { get; set; }

    }
}
