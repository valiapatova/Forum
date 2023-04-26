using Forum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        public string LastName { get; set; }        
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        //public bool IsAdmin { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsUserBlocked { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment>Comments { get; set; }
    }
}
