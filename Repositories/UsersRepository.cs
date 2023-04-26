using Forum.Data;
using Forum.Data.Models;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ForumContext context;
        public UsersRepository(ForumContext context)
        {
            this.context = context;
        }

        private IQueryable<User> UserQuery
        {
            get
            {
                return this.context.Users
                    .Include(user => user.Role)
                    .Include(user => user.Posts)
                        .ThenInclude(post => post.Comments)
                    .Include(user => user.Comments)
                        .ThenInclude(comment => comment.Post);
            }
        }
        public int UserCount
        {
            get
            {
                return this.context.Users.ToList().Count;
            }
        }

        public List<User> Get()
        {
            
            return this.UserQuery.ToList();

           // return this.context.Users.ToList();
        }
        public User Get(int id)
        {
            var user = this.UserQuery.Where(u => u.Id == id).FirstOrDefault();
            //var user = this.context.Users.Where(u => u.Id == id).FirstOrDefault();
            return user ?? throw new EntityNotFoundException();
        }
        public User Get(string username)
        {
            var user = this.UserQuery.Where(u => u.Username == username).FirstOrDefault();
           //var user = this.context.Users.Where(u => u.Username == username).FirstOrDefault();
            return user ?? throw new EntityNotFoundException();
        }
        public bool Exists(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public List<User> Get(UserQueryParameters filterParameters)
        {
            string username = !string.IsNullOrEmpty(filterParameters.Username) ? filterParameters.Username.ToLowerInvariant() : string.Empty;
            string firstname = !string.IsNullOrEmpty(filterParameters.FirstName) ? filterParameters.FirstName.ToLowerInvariant() : string.Empty;
            string lastname = !string.IsNullOrEmpty(filterParameters.LastName) ? filterParameters.LastName.ToLowerInvariant() : string.Empty;
            string email = !string.IsNullOrEmpty(filterParameters.Email) ? filterParameters.Email.ToLowerInvariant() : string.Empty;

            // Filtering by style will be supported in the next demo when we learn how to load related data.
            // string style = !string.IsNullOrEmpty(filterParameters.Style) ? filterParameters.Style.ToLowerInvariant() : string.Empty;

            string sortCriteria = !string.IsNullOrEmpty(filterParameters.SortBy) ? filterParameters.SortBy.ToLowerInvariant() : string.Empty;
            string sortOrder = !string.IsNullOrEmpty(filterParameters.SortOrder) ? filterParameters.SortOrder.ToLowerInvariant() : string.Empty;

            IQueryable<User> result = this.UserQuery;
            //IEnumerable<User> result = this.context.Users;

             result = FilterByFirstName(result, firstname);
             result = FilterByLastName(result, lastname);
             result = FilterByUsername(result, username);
             result = FilterByEmail(result, email);

            // result = FilterByStyle(result, style);

             result = SortBy(result, sortCriteria);
             result = Order(result, sortOrder);

            return result.ToList();
        }
        public User Create(User user)
        {
            var createdUser = this.context.Users.Add(user);
            this.context.SaveChanges();
            return createdUser.Entity;
        }
        public User Update(int id, User user)
        {
            var userToUpdate = this.Get(id);
            //userToUpdate.Username = user.Username;
            userToUpdate.Password = user.Password;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;            

            this.context.SaveChanges();

            return this.Get(id);
        }
        public User Delete(int id)
        {
            var userToDelete = this.Get(id);
            var deletedUser = this.context.Users.Remove(userToDelete).Entity;
            this.context.SaveChanges();
            return deletedUser;
        }
         private static IQueryable<User> FilterByFirstName(IQueryable<User> result, string firstname)
         //private static IEnumerable<User> FilterByFirstName(IEnumerable<User> result, string firstname)
        {
             return result.Where(user => user.FirstName.Contains(firstname));
             // return result.Where(user => user.FirstName.Contains(firstname, StringComparison.InvariantCultureIgnoreCase));
        }
        private static IQueryable<User> FilterByLastName(IQueryable<User> result, string lastname)
        //private static IEnumerable<User> FilterByLastName(IEnumerable<User> result, string lastname)
        {
            return result.Where(user => user.LastName.Contains(lastname));

            //return result.Where(user => user.LastName.Contains(lastname, StringComparison.InvariantCultureIgnoreCase));
        }
        private static IQueryable<User> FilterByEmail(IQueryable<User> result, string email)
        //private static IEnumerable<User> FilterByEmail(IEnumerable<User> result, string email)
        {
            return result.Where(user => user.Email.Contains(email));
            //return result.Where(user => user.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase));
        }
        private static IQueryable<User> FilterByUsername(IQueryable<User> result, string username)
        //private static IEnumerable<User> FilterByUsername(IEnumerable<User> result, string username)
        {
            return result.Where(user => user.Username.Contains(username));
            //return result.Where(user => user.Username.Contains(username, StringComparison.InvariantCultureIgnoreCase));
        }

        private static IQueryable<User> SortBy(IQueryable<User> result, string sortcriteria)
        //private static IEnumerable<User> SortBy(IEnumerable<User> result, string sortcriteria)
        {
            switch (sortcriteria)
            {
                case "username":
                    return result.OrderBy(user => user.Username);
                case "firstname":
                    return result.OrderBy(user => user.FirstName);
                case "lastname":
                    return result.OrderBy(user => user.LastName);
                case "email":
                    return result.OrderBy(user => user.Email);
                //case "title":
                //	return result.orderby(user => user.posts.                   );
                default:
                    return result;
            }
        }
        private static IQueryable<User> Order(IQueryable<User> result, string sortorder)
        //private static IEnumerable<User> Order(IEnumerable<User> result, string sortorder)
        {
            return (sortorder == "desc") ? result.Reverse() : result;
        }


    }
}
