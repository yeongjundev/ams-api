using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Lesson : DomainModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(40, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        internal List<Enrolment> Enrolments { get; set; }

        internal List<AttendanceSheet> AttendanceSheets { get; set; }

        public Lesson() : base() { }
    }
}