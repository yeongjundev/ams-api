using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrolment> Enrolments { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Enrolment>()
                .HasKey(enrolment => new { enrolment.StudentId, enrolment.LessonId });
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Student)
                .WithMany(student => student.Enrolments)
                .HasForeignKey(enrolment => enrolment.StudentId);
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Lesson)
                .WithMany(lesson => lesson.Enrolments)
                .HasForeignKey(enrolment => enrolment.LessonId);

            builder.Entity<Attendance>()
                .HasKey(attendance => new { attendance.StudentId, attendance.LessonId, attendance.AttendanceSheetId });
            builder.Entity<Attendance>()
                .HasOne(attendance => attendance.Student)
                .WithMany(student => student.Attendances)
                .HasForeignKey(attendance => attendance.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Attendance>()
                .HasOne(attendance => attendance.Lesson)
                .WithMany()
                .HasForeignKey(attendance => attendance.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Attendance>()
                .HasOne(attendance => attendance.AttendanceSheet)
                .WithMany(attendanceSheet => attendanceSheet.Attendances)
                .HasForeignKey(attendance => attendance.AttendanceSheetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}