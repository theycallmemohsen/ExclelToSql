namespace ExclelToSql.Models.DTO
{
    public class TeacherChangeRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Ranking { get; set; }

        public int? Grade { get; set; }

        public string? AccountNumber { get; set; }

        public string? InsuranceNumber { get; set; }

        public string? NationalCode { get; set; }
    }
}