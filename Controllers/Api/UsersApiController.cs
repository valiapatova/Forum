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
    [Route("api/Users")]
    // [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        
        private readonly IUsersService usersService;
        private readonly AuthorizationHelper authorizationHelper;
        private readonly UserMapper mapper;



        public UsersApiController(IUsersService usersService, AuthorizationHelper authorizationHelper, UserMapper mapper)
        {
            this.usersService = usersService;
            this.authorizationHelper = authorizationHelper;
            this.mapper = mapper;
        }
        [HttpGet("")]
        public IActionResult Get([FromHeader] string userLogName, [FromQuery] UserQueryParameters filterParameters)

        //public IActionResult Get([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                var userLog = this.authorizationHelper.TryGetUser(userLogName);

                var users = this.usersService.Get(filterParameters, userLog);

                var usersToView = mapper.ConvertUsersToIncludeUsersDto(users);

               // List<UserResponseGetIncludeDto> usersToView = users.Select(u => new UserResponseGetIncludeDto(u)).ToList();

                return this.StatusCode(StatusCodes.Status200OK, usersToView);
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

        [HttpGet("{id}/posts")]
        public IActionResult GetUserInclude(int id, [FromHeader] string userLogName)
        {
            try
            {
                var userLog = this.authorizationHelper.TryGetUser(userLogName);
                var users = this.usersService.GetUserInclude(id, userLog);
                return this.StatusCode(StatusCodes.Status200OK, users);
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
        public IActionResult GetById(int id, [FromHeader] string userLogName)
        {
            try
            {
                var userLog = this.authorizationHelper.TryGetUser(userLogName);
                var user = this.usersService.Get(id, userLog);
                return this.StatusCode(StatusCodes.Status200OK, user);
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
        public IActionResult Create([FromBody] UserRequestForCreateDto dto)
        {
            User user = mapper.ConvertUserDtoToUserForCreate(dto);    

            try
            {
                user = this.usersService.Create(user);
                return this.StatusCode(StatusCodes.Status201Created, user);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader] string userLogName, [FromBody] UserRequestForUpdateDto dto)
        {
            try
            {
                User userLog = this.authorizationHelper.TryGetUser(userLogName);

                User oldUser = this.usersService.Get(id,userLog); // ?????

                User userToUpdate = this.mapper.ConvertUserDtoToUserForUpdate(dto, oldUser);


                var updatedUser = this.usersService.Update(id, userToUpdate, userLog);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
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
                var userLog = this.authorizationHelper.TryGetUser(userLogName);
                this.usersService.Delete(id, userLog);
                return this.StatusCode(StatusCodes.Status200OK);
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

