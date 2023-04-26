using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var users = new List<User>();
            users.Add(new User() { Id = 1, FirstName = "Valentina", LastName = "Patova", RoleId = 1 , Username = "valia", Password = "123",PhoneNumber = "555-555-5555", Email = "vpatova@abv.bg"});
            users.Add(new User() { Id = 3, FirstName = "Valentina", LastName = "Iordanova", RoleId = 2 , Username = "valia2", Password = "123",PhoneNumber = "777-777-7777", Email = "vi@abv.bg"});
            users.Add(new User() { Id = 2, FirstName = "Miroslav", LastName = "Ivanov", RoleId = 2 , Username = "miro", Password = "123",PhoneNumber = "666-666-6666", Email = "mivanov@abv.bg"});
            modelBuilder.Entity<User>().HasData(users);

            var roles = new List<Role>();
            roles.Add(new Role() { Id = 1 , RoleName = "admin" });
            roles.Add(new Role() { Id = 2 , RoleName = "user" });
            modelBuilder.Entity<Role>().HasData(roles);

            var posts = new List<Post>();
            posts.Add(new Post() { Id = 1, Title = "Vacation in Sunny beach", UserId = 2 , Content = "Our Vacation was amazing .. ", Likes = 3, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 2, Title = "We went to the Golden Sands", UserId = 1 , Content = "Great Time at the beach !", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 3, Title = "Lake Tahoe visit", UserId = 3, Content = "Went skiing to Lake Tahoe last week ,we had a great time", Likes = 1, CreatedOn = DateTime.Now });
            posts.Add(new Post() { Id = 4, Title = "Grand Pyramids Hotel", UserId = 2 , Content = "Super Nice place to stay", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 5, Title = "Fifth Post", UserId = 3 , Content = "Conted Of Fifth Post", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 6, Title = "Sixth Post", UserId = 1 , Content = "Conted Of Sixth Post", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 7, Title = "Seventh Post", UserId = 1 , Content = "Conted Of Seventh Post", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 8, Title = "Eight Post", UserId = 2 , Content = "Conted Of Eight Post", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 9, Title = "Ninth Post", UserId = 3 , Content = "Conted Of Ninth Post", Likes = 1, CreatedOn = DateTime.Now});
            posts.Add(new Post() { Id = 10, Title = "Tenth Post", UserId = 2 , Content = "Conted Of Tenth Post", Likes = 1, CreatedOn = DateTime.Now});
           
            posts.Add(new Post() { Id = 11, Title = "Eleventh Post", UserId = 1 , Content = "Conted Of Eleventh Post", Likes = 1, CreatedOn = DateTime.Now});
            
            modelBuilder.Entity<Post>().HasData(posts);

            var comments = new List<Comment>();
            comments.Add(new Comment { Id = 1, PostId = 1, Content = "1 Comment", UserId = 2});
            comments.Add(new Comment { Id = 2, PostId = 1, Content = "2 Comment", UserId = 1 });
            comments.Add(new Comment { Id = 3, PostId = 1, Content = "3 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 4, PostId = 2, Content = "4 Comment", UserId = 2 });
            comments.Add(new Comment { Id = 5, PostId = 2, Content = "5 Comment", UserId = 1 });
            comments.Add(new Comment { Id = 6, PostId = 2, Content = "6 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 7, PostId = 3, Content = "7 Comment", UserId = 2 });
            comments.Add(new Comment { Id = 8, PostId = 3, Content = "8 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 9, PostId = 4, Content = "9 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 10, PostId = 5, Content = "10 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 11, PostId = 5, Content = "11 Comment", UserId = 3 });
            comments.Add(new Comment { Id = 12, PostId = 5, Content = "12 Comment", UserId = 2 });
            comments.Add(new Comment { Id = 13, PostId = 5, Content = "13 Comment", UserId = 2 });
            comments.Add(new Comment { Id = 14, PostId = 5, Content = "14 Comment", UserId = 1 });
            modelBuilder.Entity<Comment>().HasData(comments);
        }
    }
}
