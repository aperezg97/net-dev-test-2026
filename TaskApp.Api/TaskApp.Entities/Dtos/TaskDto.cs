using TaskApp.Entities.Models;

namespace TaskApp.Entities.Dtos
{
    public class TaskDto : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public Guid? AssignedToId { get; set; }
        public UserDto? AssignedTo { get; set; }
        public required Guid StatusId { get; set; }
        public StatusCatalogItemDto? Status { get; set; }

        public static TaskDto FromTask(Models.Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                AssignedToId = task.AssignedToId,
                AssignedTo = task.AssignedTo != null ? UserDto.FromUser(task.AssignedTo) : null,
                StatusId = task.StatusId,
                Status = task.Status != null ? new StatusCatalogItemDto
                {
                    Id = task.Status.Id,
                    Name = task.Status.Name,
                    Description = task.Status.Description
                } : null,
            };
        }
    }
}
