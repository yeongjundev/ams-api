using System;
using System.Collections.Generic;

namespace Core.DTOs.Serializers
{
    public class AttendanceSheetDTO
    {
        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Duration { get; set; }

        public LessonDTO Lesson { get; set; }

        public List<AttendanceForAttendanceSheetDTO> Attendances { get; set; }
    }
}