using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.DTOs
{
    public class UserResponseGetDto
    {
        public UserResponseGetDto (User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.Username;
            this.Email = user.Email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }      
        public string Email { get; set; }


        //public string PhoneNumber { get; set; }   

        //public bool IsUserBlocked { get; set; }

        //public string RoleName { get; set; }

        //public List<string> Title { get; set; }
        //public List<string> PostContent { get; set; }
        //public List<string> CommentContent { get; set; }
        //public List<string> PostToComment { get; set; }


    }
}
