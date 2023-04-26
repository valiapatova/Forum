using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class PostQueryParameters
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //public int Id { get; set; }   

        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
