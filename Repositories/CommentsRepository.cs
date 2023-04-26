using Forum.Data;
using Forum.Data.Models;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ForumContext context;

        public CommentsRepository(ForumContext context)
        {
            this.context = context;
        }
        private IQueryable<Comment> CommentQuery
        {
            get
            {
                return this.context.Comments
                    .Include(comment => comment.User)
                       .ThenInclude(user => user.Role)
                    .Include(comment => comment.Post)
                        .ThenInclude(post => post.User);
                            //.ThenInclude(user =>user.Role); // .......

            }
        }

        public List<Comment> Get()
        {
            return this.CommentQuery.ToList();

            //return this.context.Comments.ToList();
        }

        public Comment Get(int id)
        {
            var comment = this.CommentQuery.Where(comment => comment.Id == id).FirstOrDefault();

            //var comment = this.context.Comments.Where(x => x.Id == id).FirstOrDefault();

            return comment ?? throw new EntityNotFoundException();
        }

        public Comment Create(Comment comment)
        {
            var createdComment = this.context.Comments.Add(comment);
            this.context.SaveChanges();
            return createdComment.Entity;
        }

        public Comment Update(int id, Comment comment)
        {
            var commentToUpdate = this.Get(id);
            commentToUpdate.Content = comment.Content;
            this.context.SaveChanges();
            return this.Get(id);
        }

        public Comment Delete(int id)
        {
            var commentToDelete = this.Get(id);
            var deletedComment = this.context.Comments.Remove(commentToDelete).Entity;
            this.context.SaveChanges();
            return deletedComment;
        }

        public List<Comment> Get(CommentQueryParameters filterParameters)
        {
            string username = !string.IsNullOrEmpty(filterParameters.Username) ? filterParameters.Username.ToLowerInvariant() : string.Empty;
            string content = !string.IsNullOrEmpty(filterParameters.Content) ? filterParameters.Content.ToLowerInvariant() : string.Empty;

            int postId = filterParameters.PostId;
            string postTitle = !string.IsNullOrEmpty(filterParameters.PostTitle) ? filterParameters.PostTitle : string.Empty;


            string sortCriteria = !string.IsNullOrEmpty(filterParameters.SortBy) ? filterParameters.SortBy.ToLowerInvariant() : string.Empty;
            string sortOrder = !string.IsNullOrEmpty(filterParameters.SortOrder) ? filterParameters.SortOrder.ToLowerInvariant() : string.Empty;

            IQueryable<Comment> result = this.CommentQuery;

            result = FilterByUsername(result, username);
            result = FilterByContent(result, content);

            result = FilterByPostId(result, postId);
            result = FilterByPostTitle(result, postTitle);

            result = SortBy(result, sortCriteria);
            result = Order(result, sortOrder);

            return result.ToList();
        }

        private static IQueryable<Comment> FilterByContent(IQueryable<Comment> result, string content)
        {
            return result.Where(comment => comment.Content.Contains(content));
        }

        private static IQueryable<Comment> FilterByUsername(IQueryable<Comment> result, string username)
        {
            return result.Where(comment => comment.User.Username.Contains(username));
        }

        private static IQueryable<Comment> FilterByPostTitle(IQueryable<Comment> result, string postTitle)
        {
            return result.Where(comment => comment.Post.Title.Contains(postTitle));
        }
        private static IQueryable<Comment> FilterByPostId(IQueryable<Comment> result, int postId)
        {
            if (postId == 0)
            {
                return result;
            }
            return result.Where(comment => comment.PostId == postId);
        }

        private static IQueryable<Comment> SortBy(IQueryable<Comment> result, string sortcriteria)
        {
            switch (sortcriteria)
            {
                case "username":
                    return result.OrderBy(comment => comment.User.Username);
                case "content":
                    return result.OrderBy(comment => comment.Content);
                case "posttitle":
                    return result.OrderBy(comment => comment.Post.Title);
                case "postid":
                    return result.OrderBy(comment => comment.PostId);
                default:
                    return result;
            }
        }
        private static IQueryable<Comment> Order(IQueryable<Comment> result, string sortorder)
        {
            return (sortorder == "desc") ? result.Reverse() : result;
        }
    }
}
