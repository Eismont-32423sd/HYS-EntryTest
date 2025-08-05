using Domain.Entities;
using Domain.Interfaces;

namespace Infrastracture.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = [];
        private int _nextId = 1;

        public User AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id)!;
        }
    }
}
