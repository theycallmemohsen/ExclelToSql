using System;
using System.Collections.Generic;
using ExclelToSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ExclelToSql.Data;

public partial class ExclelToSqlContext : DbContext
{
    public ExclelToSqlContext()
    {
    }

    public ExclelToSqlContext(DbContextOptions<ExclelToSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Archive> Archives { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassesHistory> ClassesHistories { get; set; }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CoursesHistory> CoursesHistories { get; set; }

    public virtual DbSet<Duty> Duties { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Excel> Excels { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<Information> Information { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeachersHistory> TeachersHistories { get; set; }

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

            entity.HasOne(d => d.Course).WithMany(p => p.Classes).HasConstraintName("FK_Classes_CourseID");

            entity.HasOne(d => d.SectionNavigation).WithMany(p => p.Classes).HasConstraintName("FK_Classes_Section");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes).HasConstraintName("FK_Classes_TeacherId");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A7C32EAF2D");

            entity.Property(e => e.CourseId).ValueGeneratedNever();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
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

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rankings__3214EC07E144EAB7");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sections__3214EC07C494E503");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.PersonalCode).HasName("PK__Teachers__63E2562B4A68B111");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.DutyNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Duty");

            entity.HasOne(d => d.GradeNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Grades");

            entity.HasOne(d => d.RankingNavigation).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Rankings");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3214EC0737AF26B9");

            entity.HasOne(d => d.AuthNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLevel_Test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
