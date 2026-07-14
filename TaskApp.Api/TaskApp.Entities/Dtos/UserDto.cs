using TaskApp.Entities.Models;

namespace TaskApp.Entities.Dtos
{
    public class UserDto : BaseModel
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public IEnumerable<TaskDto> Tasks { get; set; } = new HashSet<TaskDto>();

        public static UserDto FromUser(Models.User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                // Password = user.Password,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Tasks = user.Tasks.Select(t => TaskDto.FromTask(t)).ToList()
            };
        }
    }
}
