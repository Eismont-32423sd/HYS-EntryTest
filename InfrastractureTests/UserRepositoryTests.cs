using Domain.Entities;
using Infrastracture.Persistance.Repositories;

namespace InfrastractureTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void AddUser_AssignsIdAndStoresUser()
        {
            var repo = new UserRepository();
            var user = new User { Name = "Alice" };

            var added = repo.AddUser(user);

            Assert.Equal(1, added.Id);
            Assert.Contains(added, repo.GetAllUsers());
        }

        [Fact]
        public void GetAllUsers_ReturnsAllAddedUsers()
        {
            var repo = new UserRepository();
            repo.AddUser(new User { Name = "Alice" });
            repo.AddUser(new User { Name = "Bob" });

            var all = repo.GetAllUsers();

            Assert.Equal(2, all.Count());
        }

        [Fact]
        public void GetUserById_ReturnsCorrectUser()
        {
            var repo = new UserRepository();
            var user = repo.AddUser(new User { Name = "Alice" });

            var found = repo.GetUserById(user.Id);

            Assert.NotNull(found);
            Assert.Equal(user.Id, found.Id);
            Assert.Equal("Alice", found.Name);
        }

        [Fact]
        public void GetUserById_ReturnsNullIfNotFound()
        {
            var repo = new UserRepository();

            var result = repo.GetUserById(999);

            Assert.Null(result);
        }
    }
}
