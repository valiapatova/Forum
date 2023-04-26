using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class PostResponseDto
    {
        public PostResponseDto(Post post)
        {
            this.Title = post.Title;
            this.Content = post.Content;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
