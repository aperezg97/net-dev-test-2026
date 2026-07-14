using TaskApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Data.Contexts;
using TaskApp.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TaskApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _applicationDbContext { get; set; }
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> AddUserAsync(Entities.Models.User user)
        {
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateUserAsync(User user)
        {
            _applicationDbContext.Users.Update(user);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
