using Forum.Data.Models;
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Repositories
{
    public interface ICommentsRepository
    {
        public List<Comment> Get();
        public Comment Get(int id);
        public List<Comment> Get(CommentQueryParameters filterParameters);
        public Comment Create(Comment comment);
        public Comment Update(int id, Comment comment);
        public Comment Delete(int id);
    }
}