using System;

namespace Core.DTOs.Serializers
{
    public class StudentDTO
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}