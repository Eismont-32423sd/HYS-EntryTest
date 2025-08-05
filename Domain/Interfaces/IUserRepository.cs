using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
    }
}
