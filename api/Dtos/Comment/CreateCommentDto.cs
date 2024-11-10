using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "the title must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "the title must excead 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "the content must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "the content must excead 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}