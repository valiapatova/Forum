using Forum.Data.Models;
using Forum.Models;
using System.Collections.Generic;

namespace Forum.Repositories
{
    public interface IUsersRepository
    {
        public List<User> Get();
        public User Get(int id);
        public User Get(string username);
        public bool Exists(string username);
        public User Create(User user);
        public User Update(int id, User user);
        public User Delete(int id);
        public List<User> Get(UserQueryParameters filterParameters);

    }
}