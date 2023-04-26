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
    public class PostsController : Controller
    {
        private readonly IPostsService postService;
        private readonly PostMapper postMapper;
        private readonly IUsersService userService;

        public PostsController(IPostsService postService, PostMapper postMapper, IUsersService userService)
        {
            this.postService = postService;
            this.postMapper = postMapper;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var posts = this.postService.Get();
            return this.View(model: posts);
        }

        public IActionResult Details(int id)
        {
            try
            {
                Post post = this.postService.Get(id);

                return this.View(model: post);
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Post with id {id} does not exist.";

                return this.View(viewName: "Error");
            }
        }

        public IActionResult Create()
        {
            PostViewModel viewModel = new PostViewModel();           

            return this.View(model: viewModel);
        }

        [HttpPost]
        public IActionResult Create(PostViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model: viewModel);
            }
            try
            {
                string currentUser = this.HttpContext.Session.GetString("CurrentUser");
                User user = userService.Get(currentUser);

                Post post = this.postMapper.ConvertPostViewModelToPostModelForCreate(viewModel, user);
                post.CreatedOn = DateTime.Now;
                post.User = user;  // ??
                post.UserId = user.Id;
                this.postService.Create(post);

                
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Post can not create";

                return this.View(viewName: "Error");
            }

            return this.RedirectToAction(actionName: "Index", controllerName: "Posts");
        }

        public IActionResult Edit(int id)
        {
            try
            {
                Post post = this.postService.Get(id);

                PostViewModel viewModel = this.postMapper.ConvertPostToPostViewModel(post);               

                return this.View(model: viewModel);
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Post with id {id} does not exist.";

                return this.View(viewName: "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, PostViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {              

                return this.View(model: viewModel);
            }

            try
            {
                string currentUser = this.HttpContext.Session.GetString("CurrentUser");
                User user = userService.Get(currentUser);

                if (user.IsUserBlocked)
                {
                    throw new UnauthorizedOperationException("User is blocked!");
                }

                var oldPostToUpdate = this.postService.Get(id);

                if (!(user.Id == oldPostToUpdate.UserId || user.Role.RoleName == "admin" ))
                {
                    throw new UnauthorizedOperationException($"You can not Update post with Id: {id} ");
                }


                Post post = this.postMapper.ConvertPostViewModelToPostModelForUpdate(viewModel, oldPostToUpdate);


                this.postService.Update(id, post,user);
            }
            catch (UnauthorizedOperationException)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = $"You are not authorized to edit post with Id: {id}";
                return this.View(viewName: "Error");
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Post with id {id} does not exist.";
                return this.View(viewName: "Error");
            }

            return this.RedirectToAction(actionName: "Index", controllerName: "Posts");
        }

        public IActionResult Delete(int id)
        {

            Post post = this.postService.Get(id);
            var postViewModel = postMapper.ConvertPostToPostViewModel(post);
            return this.View(postViewModel);           
        }

        [HttpPost]
        public IActionResult Delete(int id, PostViewModel viewModel)
        {
            try
            {
                string currentUser = this.HttpContext.Session.GetString("CurrentUser");
                User user = userService.Get(currentUser);

                //Post post = this.postService.Get(id);

                this.postService.Delete(id, user);

                return this.RedirectToAction(actionName: "Index", controllerName: "Posts");
            }
            catch (UnauthorizedOperationException)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = $"You are not authorized to delete post with Id: {id}";
                return this.View(viewName: "Error");
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Post with id {id} does not exist.";
                return this.View(viewName: "Error");
            }

        }

    }
}
