using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Enrolment : DomainModel
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
    }
}