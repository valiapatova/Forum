using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class PostResponseGetDto
    {
        public PostResponseGetDto(Post post)
        {
            this.Title = post.Title;
            this.Content = post.Content;
            this.PostId = post.Id;
            this.CreatedOn = post.CreatedOn;

            this.ByFirstName = post.User.FirstName;
            this.ByLastName = post.User.LastName;
            if (!(post.Comments == null))
            {
                foreach (var comment in post.Comments)
                {
                    this.WithCommentsContent.Add(comment.Content, comment.User.Username);
                }
            }
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ByFirstName { get; set; }
        public string ByLastName { get; set; }

        public Dictionary<string, string> WithCommentsContent { get; set; } = new Dictionary<string, string>();
    }
}
