using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class CommentRequestDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
