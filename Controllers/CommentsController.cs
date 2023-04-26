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
    public class CommentsController:Controller
    {
        private readonly ICommentsService commentService;
        private readonly CommentMapper commentMapper;
        private readonly IUsersService userService;


        public CommentsController(ICommentsService commentService, CommentMapper commentMapper,IUsersService userService)
        {
            this.commentMapper = commentMapper;
            this.commentService = commentService;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var comments = this.commentService.Get();
            return this.View(model: comments);
        }

        public IActionResult Edit(int id)
        {
            try
            {
                Comment comment = this.commentService.Get(id);

                CommentViewModel viewModel = this.commentMapper.ConvertCommentToCommentViewModel(comment);

                return this.View(model: viewModel);
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Comment with id {id} does not exist.";

                return this.View(viewName: "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, CommentViewModel viewModel)
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

                var oldCommentToUpdate = this.commentService.Get(id);

                if (!(user.Id == oldCommentToUpdate.UserId || user.Role.RoleName == "admin"))
                {
                    throw new UnauthorizedOperationException($"You can not Update comment with Id: {id} ");
                }


                Comment commentToUpdate = this.commentMapper.ConvertCommentViewModelToCommentModelForUpdate(viewModel, oldCommentToUpdate);


                this.commentService.Update(id, commentToUpdate, user);
            }
            catch (UnauthorizedOperationException)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = $"You are not authorized to edit comment with Id: {id}";
                return this.View(viewName: "Error");
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = $"Comment with id {id} does not exist.";
                return this.View(viewName: "Error");
            }

            return this.RedirectToAction(actionName: "Index", controllerName: "Comments");
        }








        //public IActionResult Create (int postId)
        //{

        //}
    }
}
