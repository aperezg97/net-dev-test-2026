using TaskApp.Entities.Models;

namespace TaskApp.Data.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User> AddUserAsync(Entities.Models.User user);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<List<User>> GetUsersAsync();
        public System.Threading.Tasks.Task UpdateUserAsync(User user);
    }
}
