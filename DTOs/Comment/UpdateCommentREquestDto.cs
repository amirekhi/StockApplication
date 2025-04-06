using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Comment
{
    public class UpdateCommentREquestDto
    {

        [Required]
        [MinLength(5, ErrorMessage = "Title must be over 5 characters")]
        [MaxLength(280, ErrorMessage = "Title must be under 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Title must be over 5 content")]
        [MaxLength(280, ErrorMessage = "Title must be under 280 content")]
        public string Content { get; set; } = string.Empty;
    }
}
