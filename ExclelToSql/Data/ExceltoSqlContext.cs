using System;
using System.Collections.Generic;
using ExclelToSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Data;

public partial class ExceltoSqlContext : DbContext
{
    public ExceltoSqlContext()
    {
    }

    public ExceltoSqlContext(DbContextOptions<ExceltoSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archive> Archives { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassesHistory> ClassesHistories { get; set; }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<Cooperation> Cooperations { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CoursesHistory> CoursesHistories { get; set; }

    public virtual DbSet<Duty> Duties { get; set; }

    public virtual DbSet<EducationalGroup> EducationalGroups { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Excel> Excels { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<Information> Information { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<NewFee> NewFees { get; set; }

    public virtual DbSet<Nullteacher> Nullteachers { get; set; }

    public virtual DbSet<OtherTeacher> OtherTeachers { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeachersHistory> TeachersHistories { get; set; }

    public virtual DbSet<TempSection> TempSections { get; set; }

    public virtual DbSet<TempTeacher> TempTeachers { get; set; }

    public virtual DbSet<TempTeachers2> TempTeachers2s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLevel> UserLevels { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Archive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Archieve");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC070F7A4B2E");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes).HasConstraintName("FK_Classes_Courses");

            entity.HasOne(d => d.NationalCodeNavigation).WithMany(p => p.Classes).HasConstraintName("FK_Classes_Teachers");

            entity.HasOne(d => d.SectionNavigation).WithMany(p => p.Classes).HasConstraintName("FK_Classes_Sections");

            entity.HasOne(d => d.SemesterCodeNavigation).WithMany(p => p.Classes).HasConstraintName("FK_Classes_Semester");
        });

        modelBuilder.Entity<Cooperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cooperat__3214EC07A21A49B6");
        });

        modelBuilder.Entity<Duty>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.DutyGroup).WithMany(p => p.Duties).HasConstraintName("FK_Duty_Rankings");
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.Grade).HasName("PK__Fee__DF0ADB7B3E880C34");
        });

        modelBuilder.Entity<Information>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NewInformation");

            entity.HasOne(d => d.FacultyNavigation).WithMany(p => p.Majors).HasConstraintName("FK_NewInformation_Faculty");

            entity.HasOne(d => d.Group).WithMany(p => p.Majors).HasConstraintName("FK_NewInformation_EducationalGroup");
        });

        modelBuilder.Entity<Nullteacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NullTeachers");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rankings__3214EC07E144EAB7");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sections__3214EC07C494E503");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC0728B0C663");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.NationalCode).HasName("PK__Teachers__63E2562B4A68B111");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CooperationTypeNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Cooperation");

            entity.HasOne(d => d.DutyNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Duty");

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Fee");

            entity.HasOne(d => d.Grade1).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_NewFee");

            entity.HasOne(d => d.GroupCodeNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_EducationalGroup");

            entity.HasOne(d => d.RankingNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Rankings");

            entity.HasOne(d => d.SemesterCodeNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Semester");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(d => d.AuthNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLevel_Test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
