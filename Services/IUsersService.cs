using Forum.Data.Models;
using Forum.DTOs;
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Services
{
    public interface IUsersService
    {
        public List<User> Get();
        public List<User> Get(User userLog);

        public UserResponseGetDto GetUserInclude(int id);
        public UserResponseGetIncludeDto GetUserInclude(int id, User userLog);

        public User Get(int id);
        public User Get(int id, User userLog);

        public User Get(string username);
        public bool Exists(string username);

        public List<User> Get(UserQueryParameters filterParameters);
        public List<User> Get(UserQueryParameters filterParameters, User userLog);

        public User Create(User user);
        public User Update(int id, User user, User userLog);
        public void Delete(int userId, User user);
    }
}