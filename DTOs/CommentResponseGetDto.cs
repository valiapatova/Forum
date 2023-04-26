using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class CommentResponseGetDto
    {
        public CommentResponseGetDto(Comment comment)
        {
            this.CommentContant = comment.Content;
            this.CommentId = comment.Id;
            this.ToPostTitle = comment.Post.Title;
            this.ToPostContent = comment.Post.Content;
            this.PostId = comment.PostId;
            this.ByUsername = comment.User.Username;
            this.ByFirstName = comment.User.FirstName;
            this.ByLastName = comment.User.LastName;
        }

        public string CommentContant { get; set; }
        public int CommentId { get; set; }
        public string ToPostTitle { get; set; }
        public string ToPostContent { get; set; }
        public int PostId { get; set; }
        public string ByUsername { get; set; }
        public string ByFirstName { get; set; }
        public string ByLastName { get; set; }

    }
}
