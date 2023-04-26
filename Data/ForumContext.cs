using Forum.Data.Models;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class ForumContext:DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Post>()
                .HasOne(post => post.User)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            //modelBuilder
            //    .Entity<Post>()
            //    .HasMany(post => post.Comments)
            //    .WithOne(comment => comment.Post)
            //    .HasForeignKey(post => post.PostId)
            //    .OnDelete(DeleteBehavior.Cascade);  //??????

            modelBuilder
                 .Entity<User>()
                 .HasMany(user => user.Posts)
                 .WithOne(post => post.User)
                 .HasForeignKey(user => user.UserId);
                //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Comment>()
                .HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Comment>()
                .HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Seed();
        }

    }
}
