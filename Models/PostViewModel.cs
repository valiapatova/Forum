using Forum.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class PostViewModel : PostRequestDto
    {
        public int UserId {get; set;}
       

    }
}
