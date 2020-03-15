using System;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Core.DTOs.Deserializers
{
    public class PutAttendanceDTO
    {
        [Required]
        public string AttendanceType { get; set; }
    }
}