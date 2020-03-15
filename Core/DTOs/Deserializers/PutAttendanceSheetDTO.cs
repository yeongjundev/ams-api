using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Deserializers
{
    public class PutAttendanceSheetDTO
    {
        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}