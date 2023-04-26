using Forum.Data.Models;
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Services
{
    public interface ICommentsService
    {
        public List<Comment> Get();
        public Comment Get(int id);
        public List<Comment> Get(CommentQueryParameters filterParameters, User userLog);
        public Comment Create(Comment comment);
        public Comment Update(int id, Comment comment, User user);
        public Comment Delete(int id, User user);
    }
}