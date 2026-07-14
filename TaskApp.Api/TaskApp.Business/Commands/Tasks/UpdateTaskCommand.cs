using TaskApp.Entities.Dtos;

namespace TaskApp.Business.Commands.Tasks
{
    public record UpdateTaskCommand(TaskDto taskData) : MediatR.IRequest<BaseResponseDto<TaskDto>>;
    public class UpdateTaskCommandHandler : MediatR.IRequestHandler<UpdateTaskCommand, BaseResponseDto<TaskDto>>
    {
        private readonly TaskApp.Data.Interfaces.Repositories.ITaskRepository _taskRepository;
        public UpdateTaskCommandHandler(TaskApp.Data.Interfaces.Repositories.ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public System.Threading.Tasks.Task<BaseResponseDto<TaskDto>> Handle(UpdateTaskCommand request, System.Threading.CancellationToken cancellationToken)
        {
            var currentTask = _taskRepository.GetTaskById(request.taskData.Id);
            if (currentTask == null)
            {
                return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<TaskDto>()
                {
                    Success = false,
                    Message = $"Task with ID: {request.taskData.Id} was not found."
                });
            }
            currentTask.Name = request.taskData.Name;
            currentTask.Description = request.taskData.Description;
            currentTask.DueDate = request.taskData.DueDate;
            currentTask.StatusId = request.taskData.StatusId;
            currentTask.AssignedToId = request.taskData.AssignedToId;
            currentTask.UpdatedAt = DateTime.UtcNow;
            _taskRepository.UpdateTask(currentTask);
            var taskRes = TaskDto.FromTask(currentTask);
            return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<TaskDto>()
            {
                Data = taskRes,
                Success = true,
                Message = "Task updated successfully"
            });
        }
    }
}
