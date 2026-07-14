using TaskApp.Entities.Models;

namespace TaskApp.Data.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(Entities.Models.User user);
        public User? GetUserByUsername(string username);
        public User? GetUserById(Guid id);
        public void UpdateUser(User user);
    }
}
