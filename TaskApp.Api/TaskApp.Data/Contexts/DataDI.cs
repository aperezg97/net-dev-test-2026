using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskApp.Data.Interfaces;
using TaskApp.Data.Interfaces.Repositories;
using TaskApp.Data.Repositories;
using TaskApp.Data.Seeds;
using TaskApp.Data.Utils;

namespace TaskApp.Data.Contexts
{
    public static class DataDI
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher<Entities.Models.User>, PasswordHasher<Entities.Models.User>>();
            services.AddSingleton<IPasswordHelper, PasswordHelper>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<DataSeeder>();
        }

        public static async Task SeedDataAsync(IServiceScope serviceScope)
        {
            var seeder = serviceScope.ServiceProvider.GetRequiredService<DataSeeder>();
            await seeder.SeedAsync();
        }
    }
}
