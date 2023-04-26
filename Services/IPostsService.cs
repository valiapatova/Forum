using Forum.Data.Models;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
   public interface IPostsService
    {
        public List<Post> Get();
        public Post Get(int id);        
        public List<Post> Get(User userLog);
        public Post Create(Post post);
        public Post Update(int id, Post post, User user);
        public Post Delete(int id, User user);
        public List<Post> GetTopTen();
        public List<Post> Get(PostQueryParameters filterParameters, User userLog);
    }
}
