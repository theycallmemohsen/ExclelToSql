using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExclelToSql.Migrations
{
    /// <inheritdoc />
    public partial class SecondExcel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsentNumber",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "AbsentSum",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "Calculation",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "ClassCalander",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CompensatoryClass",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CooperationType",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CourseMultipiler",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CourseMultipilerSum",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "IntegrationStatus",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "OtherTeachers",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Columns");

            migrationBuilder.DropColumn(
                name: "PracticalHour",
                table: "Columns");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Columns",
                newName: "TheoreticalAmount");

            migrationBuilder.RenameColumn(
                name: "TuitionFee",
                table: "Columns",
                newName: "Theoretical");

            migrationBuilder.RenameColumn(
                name: "TheoryHour",
                table: "Columns",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "TheoreticalUnits",
                table: "Columns",
                newName: "RowNumber");

            migrationBuilder.RenameColumn(
                name: "TeacherPaySum",
                table: "Columns",
                newName: "Ranking");

            migrationBuilder.RenameColumn(
                name: "SignedNumber",
                table: "Columns",
                newName: "Practical");

            migrationBuilder.RenameColumn(
                name: "Semester",
                table: "Columns",
                newName: "NationalNumber");

            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "Columns",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PresentationCode",
                table: "Columns",
                newName: "GrossAmount");

            migrationBuilder.RenameColumn(
                name: "PresentaionType",
                table: "Columns",
                newName: "CreditsCoefficientsSum");

            migrationBuilder.RenameColumn(
                name: "PracticalUnits",
                table: "Columns",
                newName: "CreditsCoefficients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TheoreticalAmount",
                table: "Columns",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "Theoretical",
                table: "Columns",
                newName: "TuitionFee");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Columns",
                newName: "TheoryHour");

            migrationBuilder.RenameColumn(
                name: "RowNumber",
                table: "Columns",
                newName: "TheoreticalUnits");

            migrationBuilder.RenameColumn(
                name: "Ranking",
                table: "Columns",
                newName: "TeacherPaySum");

            migrationBuilder.RenameColumn(
                name: "Practical",
                table: "Columns",
                newName: "SignedNumber");

            migrationBuilder.RenameColumn(
                name: "NationalNumber",
                table: "Columns",
                newName: "Semester");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Columns",
                newName: "Rank");

            migrationBuilder.RenameColumn(
                name: "GrossAmount",
                table: "Columns",
                newName: "PresentationCode");

            migrationBuilder.RenameColumn(
                name: "CreditsCoefficientsSum",
                table: "Columns",
                newName: "PresentaionType");

            migrationBuilder.RenameColumn(
                name: "CreditsCoefficients",
                table: "Columns",
                newName: "PracticalUnits");

            migrationBuilder.AddColumn<string>(
                name: "AbsentNumber",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbsentSum",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calculation",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassCalander",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompensatoryClass",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CooperationType",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseMultipiler",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseMultipilerSum",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseType",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntegrationStatus",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherTeachers",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PracticalHour",
                table: "Columns",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
