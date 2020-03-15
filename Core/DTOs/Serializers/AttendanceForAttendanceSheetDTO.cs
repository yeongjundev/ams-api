using System;

namespace Core.DTOs.Serializers
{
    public class AttendanceForAttendanceSheetDTO
    {
        public StudentDTO Student { get; set; }

        public string Comment { get; set; }

        public string AttendanceType { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}