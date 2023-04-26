using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class UserResponseGetIncludeDto
    {
        public UserResponseGetIncludeDto(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.RoleName = user.Role.RoleName;
            this.Title = user.Posts.Select(post => post.Title).ToList();
            this.PostContent = user.Posts.Select(post => post.Content).ToList();
            this.CommentContent = user.Comments.Select(comment => comment.Content).ToList();
            this.PostToComment = user.Comments.Select(comment => comment.Post.Title).ToList();

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsUserBlocked { get; set; }

        public string RoleName { get; set; }

        public List<string> Title { get; set; }
        public List<string> PostContent { get; set; }
        public List<string> CommentContent { get; set; }
        public List<string> PostToComment { get; set; }
    }
}
