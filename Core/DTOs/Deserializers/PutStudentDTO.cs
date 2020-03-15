using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Deserializers
{
    public class PutStudentDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Middlename { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Description { get; set; }
    }
}