using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExclelToSql.Migrations
{
    /// <inheritdoc />
    public partial class NewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theoretical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Practical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditsCoefficients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditsCoefficientsSum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeachingExtra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheoreticalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excels");
        }
    }
}
