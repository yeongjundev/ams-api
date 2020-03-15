using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Student : DomainModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public List<Enrolment> Enrolments { get; set; }

        public Student() : base() { }
    }
}