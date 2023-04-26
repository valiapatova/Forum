using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 16, ErrorMessage = "{0} must be between {2} and {1}")]
        public string Title { get; set; }
        [Required]
        [StringLength(8192, MinimumLength = 32, ErrorMessage = "{0} must be between {2} and {1}")]
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
