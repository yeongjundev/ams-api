using System;

namespace Core.DTOs.Serializers
{
    public class AttendanceDTO
    {
        public LessonDTO Lesson { get; set; }

        public StudentDTO Student { get; set; }

        public string Comment { get; set; }

        public string AttendanceType { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}