using TaskApp.Entities.Dtos;

namespace TaskApp.Business.Commands.Tasks
{
    public record UpdateTaskRangeCommand(List<TaskDto> tasksData) : MediatR.IRequest<BaseResponseDto<List<TaskDto>>>;
    public class UpdateTaskRangeCommandHandler : MediatR.IRequestHandler<UpdateTaskRangeCommand, BaseResponseDto<List<TaskDto>>>
    {
        private readonly TaskApp.Data.Interfaces.Repositories.ITaskRepository _taskRepository;
        public UpdateTaskRangeCommandHandler(TaskApp.Data.Interfaces.Repositories.ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public System.Threading.Tasks.Task<BaseResponseDto<List<TaskDto>>> Handle(UpdateTaskRangeCommand request, System.Threading.CancellationToken cancellationToken)
        {
            var tasksParam = request.tasksData.DistinctBy(x => x.Id).ToList();
            var tasksToBeUpdated = new List<Entities.Models.Task>();
            foreach (var taskParam in tasksParam)
            {
                var currentTask = _taskRepository.GetTaskById(taskParam.Id);
                if (currentTask == null)
                {
                    return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<List<TaskDto>>()
                    {
                        Success = false,
                        Message = $"Task with ID: {taskParam.Id} was not found."
                    });
                }
                currentTask.Name = taskParam.Name;
                currentTask.Description = taskParam.Description;
                currentTask.DueDate = taskParam.DueDate;
                currentTask.StatusId = taskParam.StatusId;
                currentTask.AssignedToId = taskParam.AssignedToId;
                currentTask.UpdatedAt = DateTime.UtcNow;

                tasksToBeUpdated.Add(currentTask);
            }
            var updateResult = _taskRepository.UpdateTaskRange(tasksToBeUpdated);
            var result = updateResult.Select(task => TaskDto.FromTask(task)).ToList();
            return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<List<TaskDto>>()
            {
                Data = result,
                Success = true,
                Message = "Tasks updated successfully"
            });
        }
    }
}
