using Forum.Controllers.Helpers;
using Forum.Data.Models;
using Forum.DTOs;
using Forum.Exceptions;
using Forum.Mappers;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers.Api
{
    [Route("api/posts")]
   // [Route("api/[controller]")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly IPostsService postsService;
        private readonly AuthorizationHelper authorizationHelper;
        private readonly PostMapper mapper;

        public PostsApiController(IPostsService postsService, AuthorizationHelper authorizationHelper, PostMapper mapper)
        {
            this.postsService = postsService;
            this.authorizationHelper = authorizationHelper;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get([FromHeader] string userLogName, [FromQuery] PostQueryParameters filterParameters)
        {
            if (userLogName==null)
            {
                var topTenPosts = this.postsService.GetTopTen();                
                List<PostResponseGetTopTenDto> postsToView = this.mapper.ConvertPostsToPostsTopTenDto(topTenPosts);

                return Ok(postsToView);
            }
            try
            {

                User userLog = this.authorizationHelper.TryGetUser(userLogName);
                if (userLog.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var allPosts = this.postsService.Get(filterParameters, userLog);         

                List<PostResponseGetDto> postsToView = this.mapper.ConvertPostsToPostsResponseGetDto(allPosts);                

                return Ok(postsToView);

            }
            catch (Exception)
            {
                var topTenPosts = this.postsService.GetTopTen();                

                List<PostResponseGetTopTenDto> postsToView = this.mapper.ConvertPostsToPostsTopTenDto(topTenPosts);
                //List<PostResponseGetByIdDto> postsToView = this.mapper.ConvertPostsToPostsByIdDto(topTenPosts);

                return Ok(postsToView);
            }        
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromHeader] string userLogName, int id)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }
                var post = this.postsService.Get(id);
                var postToView = mapper.ConvertPostToPostResponseGetDto(post);

                return this.StatusCode(StatusCodes.Status200OK, postToView);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromHeader] string userLogName, [FromBody] PostRequestDto dto)
        {
            try
            {
                // TryGetUser() is called only to verify that there exists a user
                // with a username equal to the authorization header.

                var user=this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var post = this.mapper.ConvertPostRequestDtoToPostModelForCreate(dto);
                post.User = user;
                post.UserId = user.Id;
                post.CreatedOn = DateTime.Now;

                var createdPost = this.postsService.Create(post);
                var postToView = this.mapper.ConvertPostToPostResponseGetDto(createdPost);

                return this.StatusCode(StatusCodes.Status201Created, postToView);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string userLogName, [FromBody] PostRequestDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var oldPostToUpdate = this.postsService.Get(id);

                if (!(user.Id == oldPostToUpdate.UserId) )
                {
                    throw new UnauthorizedOperationException($"You can not Update post with Id: {id} ");
                }

                var postToUpdate = this.mapper.ConvertPostRequestDtoToPostModelForUpdate(dto, oldPostToUpdate);        

                //postToUpdate.Id = id;
                //postToUpdate.User = user;
                //postToUpdate.UserId = user.Id;
                //postToUpdate.Comments = user.Comments.Where(c => c.PostId == id).ToList();


                var updatedPost = this.postsService.Update(id, postToUpdate, user);
                var postToView = this.mapper.ConvertPostToPostResponseGetDto(updatedPost);

                return this.StatusCode(StatusCodes.Status200OK, postToView);

            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string userLogName)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }
                var deletedPost = this.postsService.Delete(id, user);

                var postToView = this.mapper.ConvertPostToPostResponseGetDto(deletedPost);

                return this.StatusCode(StatusCodes.Status200OK, postToView);
               
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }





    }    
}
