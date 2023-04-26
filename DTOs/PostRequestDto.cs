using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class PostRequestDto
    {

        [Required]
        [StringLength(64, MinimumLength = 16, ErrorMessage = "{0} must be between {2} and {1}")]
        public string Title { get; set; }

        [Required]
        [StringLength(8192, MinimumLength = 32, ErrorMessage = "{0} must be between {2} and {1}")]
        public string Content { get; set; }

        //[Required(ErrorMessage = "The {0} field is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "The {0} field must be in the range from {1} to {2}.")]
        //public int UserId { get; set; }       
    }
}
