using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrolment> Enrolments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Enrolment>()
                .HasKey(enrolment => new { enrolment.LessonId, enrolment.StudentId });
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Student)
                .WithMany(student => student.Enrolments)
                .HasForeignKey(enrolment => enrolment.StudentId);
            builder.Entity<Enrolment>()
                .HasOne(enrolment => enrolment.Lesson)
                .WithMany(lesson => lesson.Enrolments)
                .HasForeignKey(enrolment => enrolment.LessonId);
        }
    }
}