using Forum.Data.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class HomeController:Controller
    {
        private readonly IPostsService postService;
        public HomeController(IPostsService postService)
        {
            this.postService = postService;
        }
        public IActionResult Index()
        {
            List<Post> posts = this.postService.GetTopTen();

            return this.View(model: posts);
        }
        public IActionResult About()
        {
            return this.View();
        }
    }
    
}
