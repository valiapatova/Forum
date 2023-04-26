using Forum.Controllers.Helpers;
using Forum.Data;
using Forum.Mappers;
using Forum.Repositories;
using Forum.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(option => { option.SwaggerDoc("v1", new OpenApiInfo { Title = "NEWWEBAPITEST", Version = "v1" }); });
            //EF
            services.AddDbContext<ForumContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //services.AddControllers();

            // Session
            services.AddSession(options =>
            {
                // With IdleTimeout you can change the number of seconds after which the session expires.
                // The seconds reset every time you access the session.
                // This only applies to the session, not the cookie.
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            

            services.AddTransient<UserMapper>();
            services.AddTransient<PostMapper>();
            services.AddTransient<CommentMapper>();

            services.AddTransient<AuthorizationHelper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enables the views to use resources from wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1"); });
        }
    }
}


