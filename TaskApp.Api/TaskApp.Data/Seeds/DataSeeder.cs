using TaskApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Contexts;
using TaskApp.Data.Interfaces;

namespace TaskApp.Data.Seeds
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        public readonly IPasswordHelper _passwordHelper;

        // Normal Constructor Dependency Injection works perfectly here
        public DataSeeder(ApplicationDbContext context, IPasswordHelper passwordHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
        }

        public async System.Threading.Tasks.Task SeedAsync()
        {
            // Ensure database exists/is migrated
            await _context.Database.MigrateAsync();

            var modified = false;

            var dateNow = DateTimeOffset.Parse("2026-07-12T00:00:00.000Z").UtcDateTime;
            var statusCreated = new StatusCatalogItem { Id = Guid.Parse("F6E89617-063F-4296-9A9D-A1955931C3B4"), Name = "Created", Description = "Task has been created", IsActive = true, CreatedAt = dateNow, UpdatedAt = dateNow };
            var statusPending = new StatusCatalogItem { Id = Guid.Parse("83F3B61A-8BE3-4538-9857-C9525826AAF5"), Name = "Pending", Description = "Task is pending", IsActive = true, CreatedAt = dateNow, UpdatedAt = dateNow };
            var statusInProgress = new StatusCatalogItem { Id = Guid.Parse("F766A0C6-3019-44CF-AE30-5D4142C3D1E6"), Name = "In Progress", Description = "Task is in progress", IsActive = true, CreatedAt = dateNow, UpdatedAt = dateNow };
            var statusCompleted = new StatusCatalogItem { Id = Guid.Parse("48036C69-08C4-4214-AF4C-790B3B6A5FED"), Name = "Completed", Description = "Task is completed", IsActive = true, CreatedAt = dateNow, UpdatedAt = dateNow };

            if (!await _context.StatusCatalogItems.AnyAsync())
            {
                var statusItems = new List<StatusCatalogItem>
                {
                    statusCreated,
                    statusPending,
                    statusInProgress,
                    statusCompleted,
                };
                _context.StatusCatalogItems.AddRange(statusItems);
                modified = true;
            }

            var adminUser = new User { Id = Guid.Parse("c751c027-eb8e-4d20-9400-59b5205edb38"), Username = "admin", Password = "", FirstName = "Admin", LastName = "User", Email = "user1@demo.com", IsActive = true };
            adminUser.Password = _passwordHelper.HashPassword(adminUser, "Admin!123");
            var user1 = new User { Id = Guid.Parse("caed9713-9139-41aa-bfa2-56ba33f00ec8"), Username = "john.doe", Password = "", FirstName = "John", LastName = "Doe", Email = "user2@demo.com", IsActive = true };
            user1.Password = _passwordHelper.HashPassword(user1, "Admin!123");
            var user2 = new User { Id = Guid.Parse("d021bfa5-5ea2-46d1-b5e7-394aa8319578"), Username = "jane.doe", Password = "", FirstName = "Jane", LastName = "Doe", Email = "user3@demo.com", IsActive = true };
            user2.Password = _passwordHelper.HashPassword(user2, "Admin!123");

            if (!await _context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    adminUser,
                    user1,
                    user2
                };
                _context.Users.AddRange(users);
                modified = true;
            }

            var task1 = new Entities.Models.Task() { Id = Guid.Parse("e1f3c8a0-5b6d-4f9e-9c1b-2d3e4f5a6b7c"), Name = "Task 1", Description = "This is task 1", DueDate = dateNow.AddDays(7), AssignedToId = user1.Id, StatusId = statusCreated.Id, IsActive = true };
            var task2 = new Entities.Models.Task() { Id = Guid.Parse("f2a4b5c6-7d8e-9f0a-1b2c-3d4e5f6a7b8c"), Name = "Task 2", Description = "This is task 2", DueDate = dateNow.AddDays(14), AssignedToId = user2.Id, StatusId = statusPending.Id, IsActive = true };
            var task3 = new Entities.Models.Task() { Id = Guid.Parse("a3b4c5d6-7e8f-9a0b-1c2d-3e4f5a6b7c8d"), Name = "Task 3", Description = "This is task 3", DueDate = dateNow.AddDays(21), AssignedToId = user1.Id, StatusId = statusInProgress.Id, IsActive = true };
            var task4 = new Entities.Models.Task() { Id = Guid.Parse("b4c5d6e7-8f9a-0b1c-2d3e-4f5a6b7c8d9e"), Name = "Task 4", Description = "This is task 4", DueDate = dateNow.AddDays(28), AssignedToId = user2.Id, StatusId = statusCompleted.Id, IsActive = true };

            if (!await _context.Tasks.AnyAsync())
            {
                var tasks = new List<Entities.Models.Task>()
                {
                    task1,
                    task2,
                    task3,
                    task4
                };
                _context.Tasks.AddRange(tasks);
                modified = true;
            }

            if (modified)
            {
                await _context.SaveChangesAsync();
            }
        }
    }

}
