using Forum.Data.Models;
using Forum.Exceptions;
using Forum.Models;
using Forum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class CommentsService : ICommentsService
    {
        private const string MODIFY_COMMENT_ERROR_MESSAGE = "Only admins or user who posted the comment can update or delete the comment";
        private const string VIEW_COMMENT_ERROR_MESSAGE = "Only registred not blocked user can view the comment";
        private readonly ICommentsRepository repository;
        public CommentsService(ICommentsRepository repository)
        {
            this.repository = repository;
        }

        public List<Comment> Get()
        {
            return this.repository.Get();
        }
        public Comment Get(int id)
        {
            return this.repository.Get(id);
        }
        public List<Comment> Get(CommentQueryParameters filterParameters, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))  // && userLog.IsUserBlocked)                                                                                              
            {
                throw new UnauthorizedOperationException(VIEW_COMMENT_ERROR_MESSAGE);
            }
            return this.repository.Get(filterParameters);
        }

        public Comment Create(Comment comment)
        { 
            var createdComment = this.repository.Create(comment);
            return createdComment;
        }

        public Comment Update(int id, Comment comment, User user )
        {            
            if (!(comment.UserId == user.Id))
            {
                throw new UnauthorizedOperationException(MODIFY_COMMENT_ERROR_MESSAGE);
            }     
            var updatedComment = this.repository.Update(id, comment);
            return updatedComment;
        }
        public Comment Delete(int id, User user)
        {
            var comment = this.repository.Get(id);
            if (!(user.Role.RoleName == "admin") || !(comment.UserId == user.Id))
            
            {
                throw new UnauthorizedOperationException(MODIFY_COMMENT_ERROR_MESSAGE);
            }

            var deletedComment=this.repository.Delete(id);
            return deletedComment;
        }

    }
}
