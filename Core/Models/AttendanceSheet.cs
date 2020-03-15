using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class AttendanceSheet : DomainModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return EndDateTime - StartDateTime;
            }
        }

        [Required]
        public Guid LessonId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

        public List<Attendance> Attendances { get; set; }

        public AttendanceSheet() : base() { }
    }
}