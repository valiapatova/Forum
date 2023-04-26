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
    public class PostsRepository : IPostsRepository
    {
        private readonly ForumContext context;
        
        public PostsRepository(ForumContext context)
        {
            this.context = context;
        }

        private IQueryable<Post> PostQuery
        {
            get
            {
                return this.context.Posts
                    .Include(post => post.User)
                       .ThenInclude(user=>user.Role)
                    .Include(post => post.Comments)                        
                        .ThenInclude(comment => comment.User);
                    
            }
        }

        public List<Post> Get()
        {
            return this.PostQuery.ToList();

            //return this.context.Posts.ToList();
        }

        public Post Get(int id)
        {
            var post = this.PostQuery.Where(post => post.Id == id).FirstOrDefault();
            //var post = this.context.Posts.Where(u => u.Id == id).FirstOrDefault();
            return post ?? throw new EntityNotFoundException();
        }
        public List<Post> GetTopTen()
        {
            return this.PostQuery.OrderByDescending(post => post.Comments.Count).ThenBy(post => post.CreatedOn).Take(10).ToList();
            //return this.context.Posts.OrderByDescending(post => post.Comments.Count).ThenBy(post => post.CreatedOn).Take(10).ToList();
        }
        public Post Get(string title)
        {
            var post = this.PostQuery.Where(post => post.Title == title).FirstOrDefault();
            //var post = this.context.Posts.Where(x => x.Title == title).FirstOrDefault();
            return post ?? throw new EntityNotFoundException();
        }

        
        public Post Create(Post post)
        {
            var createdPost = this.context.Posts.Add(post);
            this.context.SaveChanges();
            return createdPost.Entity;
        }

        public Post Update(int id, Post post)
        {
            var postToUpdate = this.Get(id);
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            
            this.context.SaveChanges();

            return this.Get(id);
        }
        public Post Delete(int id)
        {
            var postToDelete = this.Get(id);
            var deletedPost = this.context.Posts.Remove(postToDelete).Entity;
            this.context.SaveChanges();
            return deletedPost;
        }
         

        public List<Post> Get(PostQueryParameters filterParameters)
        {
            string username = !string.IsNullOrEmpty(filterParameters.Username) ? filterParameters.Username.ToLowerInvariant() : string.Empty;
            string title = !string.IsNullOrEmpty(filterParameters.Title) ? filterParameters.Title.ToLowerInvariant() : string.Empty;
            string content = !string.IsNullOrEmpty(filterParameters.Content) ? filterParameters.Content.ToLowerInvariant() : string.Empty;

            string sortCriteria = !string.IsNullOrEmpty(filterParameters.SortBy) ? filterParameters.SortBy.ToLowerInvariant() : string.Empty;
            string sortOrder = !string.IsNullOrEmpty(filterParameters.SortOrder) ? filterParameters.SortOrder.ToLowerInvariant() : string.Empty;

            IQueryable<Post> result = this.PostQuery;

            result = FilterByUsername(result, username);
            result = FilterByTitle(result, title);          
            result = FilterByContent(result, content);          
                             
            result = SortBy(result, sortCriteria);
            result = Order(result, sortOrder);

            return result.ToList();
        }

        private static IQueryable<Post> FilterByTitle(IQueryable<Post> result, string title)        
        {
            return result.Where(post => post.Title.Contains(title));
        }
        private static IQueryable<Post> FilterByContent(IQueryable<Post> result, string content)
        {
            return result.Where(post => post.Content.Contains(content));
        }

        private static IQueryable<Post> FilterByUsername(IQueryable<Post> result, string username)       
        {
            return result.Where(post => post.User.Username.Contains(username));            
        }

        private static IQueryable<Post> SortBy(IQueryable<Post> result, string sortcriteria)        
        {
            switch (sortcriteria)
            {
                case "username":
                    return result.OrderBy(post => post.User.Username);
                case "title":
                    return result.OrderBy(post => post.Title);
                case "content":
                    return result.OrderBy(post => post.Content);
                default:
                    return result;
            }
        }
        private static IQueryable<Post> Order(IQueryable<Post> result, string sortorder)        
        {
            return (sortorder == "desc") ? result.Reverse() : result;
        }


    }
}
