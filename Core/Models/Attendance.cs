using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Attendance : DomainModel
    {
        [Key, Column(Order = 0)]
        [Required]
        public Guid StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public int AttendanceSheetId { get; set; }

        [ForeignKey("AttendanceSheetId")]
        public AttendanceSheet AttendanceSheet { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }
    }
}