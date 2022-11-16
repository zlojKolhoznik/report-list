using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServerApp.Model
{
    public partial class ReportlistContext : DbContext
    {
        public ReportlistContext()
        {
        }

        public ReportlistContext(DbContextOptions<ReportlistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupsLesson> GroupsLessons { get; set; } = null!;
        public virtual DbSet<Homework> Homeworks { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectsTeacher> SubjectsTeachers { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.ReadFromJsonFile("appsettings.json").ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<GroupsLesson>(entity =>
            {
                entity.HasIndex(e => e.LessonsId, "IX_GroupLesson_LessonsId");

                entity.HasIndex(e => e.GroupsId, "IX_GroupsLessons_GroupsId");

                entity.HasOne(d => d.Groups)
                    .WithMany(p => p.GroupsLessons)
                    .HasForeignKey(d => d.GroupsId)
                    .HasConstraintName("FK_GroupLesson_Groups_GroupsId");

                entity.HasOne(d => d.Lessons)
                    .WithMany(p => p.GroupsLessons)
                    .HasForeignKey(d => d.LessonsId)
                    .HasConstraintName("FK_GroupLesson_Lessons_LessonsId");
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasIndex(e => e.GroupId, "IX_Homeworks_GroupId");

                entity.HasIndex(e => e.SubjectId, "IX_Homeworks_SubjectId");

                entity.HasIndex(e => e.TeacherId, "IX_Homeworks_TeacherId");

                entity.Property(e => e.FileExtension).HasMaxLength(10);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Homeworks)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Homeworks)
                    .HasForeignKey(d => d.SubjectId);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Homeworks)
                    .HasForeignKey(d => d.TeacherId);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasIndex(e => e.SubjectId, "IX_Lessons_SubjectId");

                entity.HasIndex(e => e.TeacherId, "IX_Lessons_TeacherId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SubjectId);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.TeacherId);
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasIndex(e => e.HomeworkId, "IX_Marks_HomeworkId");

                entity.HasIndex(e => e.LessonId, "IX_Marks_LessonId");

                entity.HasIndex(e => e.StudentId, "IX_Marks_StudentId");

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.HomeworkId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.LessonId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.GroupId, "IX_Students_GroupId");

                entity.HasIndex(e => e.UserId, "IX_Students_UserId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<SubjectsTeacher>(entity =>
            {
                entity.HasIndex(e => e.SubjectId, "IX_SubjectTeachers_SubjectId");

                entity.HasIndex(e => e.TeacherId, "IX_SubjectTeachers_TeacherId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectsTeachers)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_SubjectTeachers_Subjects_SubjectId");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SubjectsTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_SubjectTeachers_Teachers_TeacherId");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Teachers_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
