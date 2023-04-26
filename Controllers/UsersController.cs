using Forum.Mappers;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class UsersController:Controller
    {
        private readonly IUsersService userService;
        private readonly UserMapper userMapper;
        

        public UsersController(IUsersService userService, UserMapper userMapper)
        {            
            this.userMapper = userMapper;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var users = this.userService.Get();
            return this.View(model: users);
        }

    }
}
