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
    public class CommentMapper
    {
        private readonly IPostsService postService;
        public CommentMapper(IPostsService postService)
        {
            this.postService = postService;
        }
        public Comment ConvertCommentRequestDtoToCommentModelForCreate(CommentRequestDto dto)
        {
            Comment model = new Comment();            
            model.Content = dto.Content;
            model.Post = this.postService.Get(dto.PostId);
            model.PostId = dto.PostId;

            return model;
        }

        public Comment ConvertCommentRequestDtoToCommentModelForUpdate(CommentRequestForUpdateDto dto, Comment oldComment)
        {
            var model = oldComment;
            model.Content = dto.Content;

            //oldComment.Post = this.postService.Get(dto.PostId);           

            return model;
        }

        public Comment ConvertCommentViewModelToCommentModelForUpdate(CommentViewModel commentViewModel, Comment oldComment)
        {
            Comment model = oldComment;            
            model.Content = commentViewModel.Content;
            // model.User = this.userService.Get(dto.UserId);

            return model;
        }
        public CommentViewModel ConvertCommentToCommentViewModel(Comment comment)
        {
            return new CommentViewModel()
            {                
                Content = comment.Content,
                UserId = comment.UserId,
                PostId = comment.PostId

            };
        }


        public CommentResponseGetDto ConvertCommentToCommentResponseGetDto(Comment comment)
        {
            CommentResponseGetDto model = new CommentResponseGetDto(comment);
            return model;
        }

        public List<CommentResponseGetDto> ConvertCommentsToCommentsResponseGetDto(List<Comment> comments)
        {
            List<CommentResponseGetDto> model = comments.Select(comment => new CommentResponseGetDto(comment)).ToList();
            return model;
        }
    }
}
