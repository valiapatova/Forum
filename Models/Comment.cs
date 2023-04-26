using Forum.Data.Models;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
