using Forum.Controllers.Helpers;
using Forum.Data.Models;
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

namespace Forum.Controllers
{
    public class AuthenticationController:Controller
    {
        private readonly AuthorizationHelper authHelper;
        private readonly IUsersService usersService;
        private readonly UserMapper userMapper;
        public AuthenticationController(AuthorizationHelper authHelper, IUsersService usersService, UserMapper userMapper)
        {
            this.authHelper = authHelper;
            this.usersService = usersService;
            this.userMapper = userMapper;
        }
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return this.View(model:viewModel);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model: viewModel);
            }
            try
            {
                User user = this.authHelper.TryGetUser(viewModel.Username, viewModel.Password);
                this.HttpContext.Session.SetString(key: "CurrentUser", value: user.Username);
                this.HttpContext.Session.SetString(key: "CurrentUserRole", value: user.Role.RoleName);

                return this.RedirectToAction(actionName: "Index", controllerName: "Posts");

            }
            catch (AuthorizationException e)
            //catch (AuthenticationException e)
            {
                this.ModelState.AddModelError(key: "Username", errorMessage: e.Message);

                return this.View(model: viewModel);
            }
        }
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("CurrentUser");
            this.HttpContext.Session.Remove("CurrentUserRole");

            return this.RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();

            return this.View(model: viewModel);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model: viewModel);
            }
             
            if (this.usersService.Exists(viewModel.Username))
            {
                this.ModelState.AddModelError(key: "Username", errorMessage: "User with same username already exists.");

                return this.View(model: viewModel);
            }

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                this.ModelState.AddModelError(key: "ConfirmPassword", errorMessage: "The password and confirmation password do not match.");

                return this.View(model: viewModel);
            }

            User user = this.userMapper.Convert(viewModel);
            this.usersService.Create(user);

            return this.RedirectToAction(actionName: "Login", controllerName: "Authentication");
        }




    }
}
