using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Data.Contexts;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private ApplicationDbContext _applicationDbContext { get; set; }
        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Entities.Models.Task AddTask(Entities.Models.Task task)
        {
            _applicationDbContext.Tasks.Add(task);
            _applicationDbContext.SaveChanges();
            return task;
        }

        public Entities.Models.Task? GetTaskById(Guid id)
        {
            return _applicationDbContext.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public Entities.Models.Task UpdateTask(Entities.Models.Task task)
        {
            _applicationDbContext.Tasks.Update(task);
            _applicationDbContext.SaveChanges();
            return task;
        }

        public List<Entities.Models.Task> GetAllTasks(bool activeOnly)
        {
            var iqueryable = _applicationDbContext.Tasks.Include(x => x.AssignedTo).AsQueryable();
            if (activeOnly)
            {
                iqueryable = iqueryable.Where(t => t.IsActive);
            }
            return iqueryable.ToList();
        }

        public List<Entities.Models.Task> GetTasksByUserId(Guid userId)
        {
            return _applicationDbContext.Tasks.Where(t => t.AssignedToId == userId).ToList();
        }

        public List<Entities.Models.Task> GetTasksByStatusId(Guid statusId)
        {
            return _applicationDbContext.Tasks.Where(t => t.StatusId == statusId).ToList();
        }
    }
}
