using Forum.Data.Models;
using Forum.Exceptions;
using Forum.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers.Helpers
{
    public class AuthorizationHelper
    {
        private const string AuthorizationErrorMessage = "Only admins can update or delete.";
        private const string AuthenticationErrorMessage = "Authentication failed. Check credentials.";


        private readonly IUsersService usersService;

        public AuthorizationHelper(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        public User TryGetUser(string username)
        {
            try
            {
                return this.usersService.Get(username);
            }
            catch (EntityNotFoundException)
            {
                throw new AuthorizationException("Invalid Username");
            }
        }
        public User TryGetUser(string username, string password)    // ERROR TODO
        {
            var user = this.TryGetUser(username);
            if (user.Password != password)
            {
                throw new AuthenticationException(AuthenticationErrorMessage);
            }
            return user;
        }
        public User TryGetAdmin(string username)
        {
            User user = this.TryGetUser(username);
            if (!(user.Role.RoleName=="admin"))

            //if (!user.IsAdmin)
            {
                throw new AuthorizationException(AuthorizationErrorMessage);
            }
            return user;
        }

        public User TryGetAdmin(string username, string password)
        {
            User user = this.TryGetUser(username, password);
            if (!(user.Role.RoleName == "admin"))
            //if (!user.IsAdmin)
            {
                throw new AuthorizationException(AuthorizationErrorMessage);
            }
            return user;
        }



    }
}
