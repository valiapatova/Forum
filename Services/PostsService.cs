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
    public class PostsService:IPostsService
    {
        private const string MODIFY_POST_ERROR_MESSAGE = "You are not authorized to update or delete the post.";
        private const string VIEW_POST_ERROR_MESSAGE = "You are not authorized to view  posts.";

        private readonly IPostsRepository repository;
        public PostsService(IPostsRepository repository)
        {
            this.repository = repository;
        }
        public List<Post> Get()
        {
            return this.repository.Get();
        }

        public List<Post> Get(User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))                                                                                        
            {
                throw new UnauthorizedOperationException(VIEW_POST_ERROR_MESSAGE);
            }
            return this.repository.Get();
        }

        public List<Post> GetTopTen()
        {
            return this.repository.GetTopTen();
        }
        public Post Get(int id)
        {
            return this.repository.Get(id);
        }
        public Post Create (Post post)
        {
            bool duplicateExists = true;

            try
            {
                this.repository.Get(post.Title);
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            if (duplicateExists)
            {
                throw new DuplicateEntityException();
            }

            var createdPost = this.repository.Create(post);
            return createdPost;
        }
        public Post Update(int id, Post post, User user)
        {

            if (!(user.Role.RoleName == "admin" || user.Id==post.UserId))           
            {
                throw new UnauthorizedOperationException(MODIFY_POST_ERROR_MESSAGE);
            }

            bool duplicateExists = true;
            try
            {
                var existingPost = this.repository.Get(post.Title);
                if (existingPost.Id == post.Id)
                {
                    duplicateExists = false;
                }
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            if (duplicateExists)
            {
                throw new DuplicateEntityException();
            }

            var updatedPost = this.repository.Update(id, post);
            return updatedPost;
        }
        public Post Delete(int id, User user)
        {
            var post = this.repository.Get(id);

            if (!(user.Role.RoleName == "admin" || user.Id==post.UserId))            
            {
                throw new UnauthorizedOperationException(MODIFY_POST_ERROR_MESSAGE);
            }

            var deletedPost = this.repository.Delete(id);
            return deletedPost;
        }

        public List<Post> Get(PostQueryParameters filterParameters, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            {
                throw new UnauthorizedOperationException(VIEW_POST_ERROR_MESSAGE);
            }
            return this.repository.Get(filterParameters);
        }
    }
}
