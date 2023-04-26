using Forum.Data.Models;
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Repositories
{
    public interface IPostsRepository
    {
        public List<Post> Get();
        public Post Get(int id);
        public Post Get(string title);
        public Post Create(Post post);
        public Post Update(int id, Post post);
        public Post Delete(int id);

        public List<Post> Get(PostQueryParameters filterParameters);
        public List<Post> GetTopTen();
    }
}