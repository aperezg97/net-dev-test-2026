using TaskApp.Entities.Models;

namespace TaskApp.Entities.Dtos
{
    public class CreateUserDto
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public static User FromCreateUserDto(CreateUserDto createUserDto)
        {
            return new User
            {
                Username = createUserDto.Username,
                Password = createUserDto.Password ?? string.Empty, // Ensure password is not null
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
            };
        }
    }
}
