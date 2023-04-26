using Forum.Data.Models;
using Forum.DTOs;
using Forum.Exceptions;
using Forum.Models;
using Forum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class UsersService : IUsersService
    {
        private const string MODIFY_USER_ERROR_MESSAGE = "You can not update or delete a username - profiler.";
        private const string VIEW_USER_ERROR_MESSAGE = "You can not view username information";

        private readonly IUsersRepository repository;
        public UsersService(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public List<User> Get()
        {

            return this.repository.Get();
        }

        public List<User> Get(User userLog)       
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            {
                throw new UnauthorizedOperationException(VIEW_USER_ERROR_MESSAGE);
            }
            return this.repository.Get();           
        }

        public UserResponseGetDto GetUserInclude(int id)
        {
            var user = this.repository.Get(id);
            var userToView = new UserResponseGetDto(user);

            //List<UserResponseGetDto> userToView = users.Select(user => new UserResponseGetDto(user)).ToList();
            return userToView;

        }

        public UserResponseGetIncludeDto GetUserInclude(int id, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            {
                throw new UnauthorizedOperationException(VIEW_USER_ERROR_MESSAGE);
            }
            var user = this.repository.Get(id);
            var userToView = new UserResponseGetIncludeDto(user);
           // var userToView = new UserResponseGetDto(user);

            //List<UserResponseGetDto> userToView = users.Select(user => new UserResponseGetDto(user)).ToList();
            return userToView; 
             
        }
        public User Get(int id)
        {
            try
            {
                return this.repository.Get(id);
            }
            catch (EntityNotFoundException)
            {
                throw new EntityNotFoundException();
            }
        }
        public User Get(int id, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            {
                throw new UnauthorizedOperationException(VIEW_USER_ERROR_MESSAGE);
            }
            try
            {
                return this.repository.Get(id);
            }
            catch(EntityNotFoundException)
            {
                throw new EntityNotFoundException();
            }
        }

        public User Get (string username)
        {
            //if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            //{
            //    throw new UnauthorizedOperationException(VIEW_USER_ERROR_MESSAGE);
            //}

            return this.repository.Get(username);
        }
        public bool Exists(string username)
        {
            return this.repository.Exists(username);
        }
        public List<User> Get(UserQueryParameters filterParameters)
        {   
            return this.repository.Get(filterParameters);
        }

        public List<User> Get(UserQueryParameters filterParameters, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin                                                                                           
            {
                throw new UnauthorizedOperationException(VIEW_USER_ERROR_MESSAGE);
            }
            return this.repository.Get(filterParameters);
        }

        public User Create(User user)
        {
            bool UsernameExists = true;

            try
            {
                this.repository.Get(user.Username);
            }
            catch (EntityNotFoundException)
            {
                UsernameExists = false;
            }

            if (UsernameExists)
            {
                throw new DuplicateEntityException();
            }

            var createdUser = this.repository.Create(user);
            return createdUser;
        }

        public User Update(int id, User user, User userLog)
        {
            if (!(userLog.Role.RoleName == "user" || userLog.Role.RoleName == "admin"))   //?? admin           
            {
                throw new UnauthorizedOperationException(MODIFY_USER_ERROR_MESSAGE);
            }

            bool duplicateExists = true;
            try
            {
                var existingUser = this.repository.Get(user.Username);
                if (existingUser.Id == user.Id)
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

            var updatedUser = this.repository.Update(id, user);
            return updatedUser;
        }

        public void Delete(int userId, User user)
        {
            if (!(user.Role.RoleName == "admin"))     
                 
            {
                throw new UnauthorizedOperationException("Only admin can delete the user !");
            }
            this.repository.Delete(userId);
        }


    }
 }
