namespace TaskApp.Entities.Models
{
    public class StatusCatalogItem : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
