using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Deserializers
{
    public class PostLessonDTO
    {
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(300)]
        public string Description { get; set; }
    }
}