using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class CommentQueryParameters
    {
        public string Username { get; set; }       
        public string Content { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
