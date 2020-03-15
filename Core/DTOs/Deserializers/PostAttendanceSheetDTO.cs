using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Deserializers
{
    public class PostAttendanceSheetDTO
    {
        [Required]
        public Guid LessonId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}