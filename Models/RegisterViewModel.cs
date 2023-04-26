using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class RegisterViewModel:LoginViewModel
    {
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
