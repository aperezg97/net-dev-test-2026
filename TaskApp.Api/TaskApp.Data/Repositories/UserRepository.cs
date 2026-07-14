using TaskApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Data.Contexts;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _applicationDbContext { get; set; }
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public User AddUser(Entities.Models.User user)
        {
            _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return user;
        }

        public User? GetUserByUsername(string username)
        {
            return _applicationDbContext.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public User? GetUserById(Guid id)
        {
            return _applicationDbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();
        }
    }
}
