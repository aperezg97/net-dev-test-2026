using TaskApp.Entities.Models;

namespace TaskApp.Entities.Dtos
{
    public class StatusCatalogItemDto : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
