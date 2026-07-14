namespace TaskApp.Data.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        public Entities.Models.Task AddTask(Entities.Models.Task task);
        public Entities.Models.Task UpdateTask(Entities.Models.Task task);
        public List<Entities.Models.Task> UpdateTaskRange(List<Entities.Models.Task> tasks);
        public Entities.Models.Task? GetTaskById(Guid id);
        public List<Entities.Models.Task> GetAllTasks(bool onlyActives);
        public List<Entities.Models.Task> GetTasksByUserId(Guid id);
        public List<Entities.Models.Task> GetTasksByStatusId(Guid id);
    }
}
