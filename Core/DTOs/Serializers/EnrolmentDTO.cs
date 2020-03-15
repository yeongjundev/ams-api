using System;

namespace Core.DTOs.Serializers
{
    public class EnrolmentDTO
    {
        public StudentDTO Student { get; set; }

        public LessonDTO Lesson { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}