using TaskApp.Entities.Dtos;

namespace TaskApp.Entities.Models
{
    public class Task : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public Guid? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        public required Guid StatusId { get; set; }
        public StatusCatalogItem? Status { get; set; }

        public static Task FromTaskDto(TaskDto taskDto)
        {
            return new Task
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                AssignedToId = taskDto.AssignedToId,
                StatusId = taskDto.StatusId,
                IsActive = taskDto.IsActive,
                CreatedAt = taskDto.CreatedAt,
                UpdatedAt = taskDto.UpdatedAt,
                Status = taskDto.Status != null ? new StatusCatalogItem
                {
                    Id = taskDto.Status.Id,
                    Name = taskDto.Status.Name,
                    Description = taskDto.Status.Description
                } : null
            };
        }
    }
}
