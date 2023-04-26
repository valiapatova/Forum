using Forum.Data.Models;
using Forum.DTOs;
using Forum.Models;
using Forum.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Mappers
{
    public class PostMapper
    {

        private readonly IPostsService postService;
        public PostMapper(IPostsService postService)
        {
            this.postService = postService;
        }

        public Post ConvertPostRequestDtoToPostModelForUpdate(PostRequestDto dto, Post oldPost)
        {
            Post model = oldPost;
            model.Title = dto.Title;
            model.Content = dto.Content;
            // model.User = this.userService.Get(dto.UserId);

            return model;
        }

        public Post ConvertPostViewModelToPostModelForUpdate(PostViewModel postViewModel, Post oldPost)
        {
            Post model = oldPost;
            model.Title = postViewModel.Title;
            model.Content = postViewModel.Content;
            // model.User = this.userService.Get(dto.UserId);

            return model;
        }


        public Post ConvertPostRequestDtoToPostModelForCreate(PostRequestDto dto)
        // ConvertCommentRequestDtoToCommentModelForCreate
        {
            Post model = new Post();
            model.Title = dto.Title;
            model.Content = dto.Content;
           // model.User = this.userService.Get(dto.UserId);

            return model;
        }

        public Post ConvertPostViewModelToPostModelForCreate(PostViewModel viewModel, User user)
        // ConvertCommentRequestDtoToCommentModelForCreate
        {
            Post model = new Post();
            model.Title = viewModel.Title;
            model.Content = viewModel.Content;
            model.UserId = user.Id;
            // model.User = this.userService.Get(dto.UserId);

            return model;
        }

        public Post ConvertPostViewModelToPostModelForUpdate(PostViewModel viewModel, User user)
        // ConvertCommentRequestDtoToCommentModelForCreate
        {
            Post model = new Post();
            model.Title = viewModel.Title;
            model.Content = viewModel.Content;
            // model.UserId = user.Id;
            // model.User = this.userService.Get(dto.UserId);

            return model;
        }

        public PostViewModel ConvertPostToPostViewModel(Post post)
        {
            return new PostViewModel()
            {
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId
                
            };
        }



        public PostResponseGetTopTenDto ConvertPostToPostTopTenDto(Post post)
        {
            PostResponseGetTopTenDto model = new PostResponseGetTopTenDto(post);

            return model;
        }

        public List<PostResponseGetTopTenDto> ConvertPostsToPostsTopTenDto(List<Post> posts)
        {
            List<PostResponseGetTopTenDto > model= posts.Select(post=>new PostResponseGetTopTenDto(post)).ToList();

            return model;
        }



        public PostResponseGetDto ConvertPostToPostResponseGetDto(Post post)
        {
            PostResponseGetDto model = new PostResponseGetDto(post);

            return model;
        }

        public List<PostResponseGetDto> ConvertPostsToPostsResponseGetDto(List<Post> posts)
        {
            List<PostResponseGetDto> model = posts.Select(post => new PostResponseGetDto(post)).ToList();

            return model;
        }
    }
}
