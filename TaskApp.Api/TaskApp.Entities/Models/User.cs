using TaskApp.Entities.Dtos;

namespace TaskApp.Entities.Models
{
    public class User : BaseModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }

        public IEnumerable<Task> Tasks { get; set; } = new HashSet<Task>();

        public static User FromUserDto(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Password = userDto.Password ?? string.Empty,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                CreatedAt = userDto.CreatedAt,
                UpdatedAt = userDto.UpdatedAt,
                Tasks = userDto.Tasks.Select(t => Task.FromTaskDto(t)).ToList()
            };
        }
    }
}
