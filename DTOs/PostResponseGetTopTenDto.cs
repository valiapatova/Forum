using Forum.Data.Models;
using Forum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class PostResponseGetTopTenDto
    {        
        public PostResponseGetTopTenDto(Post post)
        {           

            this.Title = post.Title;
            this.WithId = post.Id;
            this.Content = post.Content;
            this.CreatedOn = post.CreatedOn;

            this.ByFirstName = post.User.FirstName;
            this.ByLastName = post.User.LastName;                 
         
            // this.UserCount = post.User.Id (p => p.UserId);  ??         

        }
        public string Title { get; set; }
        public int WithId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ByFirstName { get; set; }
        public string ByLastName { get; set; }

       //public int UserCount { get; set; }
    }
}
