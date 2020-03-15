using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public enum AttendanceType
    {
        Attend = 0,
        Absent = 1,
        Trial = 2
    }

    public class Attendance : DomainModel
    {
        [Key, Column(Order = 0)]
        [Required]
        public Guid StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public Guid LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public Guid AttendanceSheetId { get; set; }

        [ForeignKey("AttendanceSheetId")]
        public AttendanceSheet AttendanceSheet { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }

        public AttendanceType AttendanceType { get; set; } = AttendanceType.Absent;
    }
}