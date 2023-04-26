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
    [Route("api/comments")]
    // [Route("api/[controller]")]
    [ApiController]
    public class CommentsApiController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly AuthorizationHelper authorizationHelper;
        private readonly CommentMapper mapper;

        public CommentsApiController(ICommentsService commentsService, AuthorizationHelper authorizationHelper, CommentMapper mapper)
        {
            this.commentsService = commentsService;
            this.authorizationHelper = authorizationHelper;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get([FromHeader] string userLogName, [FromQuery] CommentQueryParameters filterParameters)
        {
            try
            {
                User userLog = this.authorizationHelper.TryGetUser(userLogName);

                if (userLog.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var allComments = this.commentsService.Get(filterParameters,userLog);

                List<CommentResponseGetDto> commentsToView = this.mapper.ConvertCommentsToCommentsResponseGetDto(allComments);

                return this.StatusCode(StatusCodes.Status200OK, commentsToView);

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

                var comment = this.commentsService.Get(id);
                var commentToView = mapper.ConvertCommentToCommentResponseGetDto(comment);

                return this.StatusCode(StatusCodes.Status200OK, commentToView);
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
        public IActionResult Create([FromHeader] string userLogName, [FromBody] CommentRequestDto dto)
        {
            try
            {
                // TryGetUser() is called only to verify that there exists a user
                // with a username equal to the authorization header.

                var user = this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var comment = this.mapper.ConvertCommentRequestDtoToCommentModelForCreate(dto);

                comment.User = user;
                comment.UserId = user.Id;               
               
                var createdComment = this.commentsService.Create(comment);
                var commentToView = this.mapper.ConvertCommentToCommentResponseGetDto(createdComment);

                return this.StatusCode(StatusCodes.Status201Created, commentToView);
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
        public IActionResult Update(int id, [FromHeader] string userLogName, [FromBody] CommentRequestForUpdateDto dto)
        {
            try
            {
                var user = this.authorizationHelper.TryGetUser(userLogName);
                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var oldCommentToUpdate = this.commentsService.Get(id);

                if (!(user.Id == oldCommentToUpdate.UserId))
                {
                    throw new UnauthorizedOperationException($"You can not Update comment with Id: {id} ");
                }

                var commentToUpdate = this.mapper.ConvertCommentRequestDtoToCommentModelForUpdate(dto, oldCommentToUpdate);            

                var updatedComment = this.commentsService.Update(id, commentToUpdate, user);

                var commentToView = this.mapper.ConvertCommentToCommentResponseGetDto(updatedComment);

                return this.StatusCode(StatusCodes.Status200OK, commentToView);

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
                var deletedComment = this.commentsService.Delete(id, user);

                var commentToView = this.mapper.ConvertCommentToCommentResponseGetDto(deletedComment);

                return this.StatusCode(StatusCodes.Status200OK, commentToView);

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
