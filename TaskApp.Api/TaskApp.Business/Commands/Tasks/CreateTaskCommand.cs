using TaskApp.Entities.Dtos;

namespace TaskApp.Business.Commands.Tasks
{
    public record CreateTaskCommand(TaskDto taskData) : MediatR.IRequest<BaseResponseDto<TaskDto>>;
    public class CreateTaskCommandHandler : MediatR.IRequestHandler<CreateTaskCommand, BaseResponseDto<TaskDto>>
    {
        private readonly TaskApp.Data.Interfaces.Repositories.ITaskRepository _taskRepository;
        public CreateTaskCommandHandler(TaskApp.Data.Interfaces.Repositories.ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public System.Threading.Tasks.Task<BaseResponseDto<TaskDto>> Handle(CreateTaskCommand request, System.Threading.CancellationToken cancellationToken)
        {
            var taskModel = Entities.Models.Task.FromTaskDto(request.taskData);
            var task = _taskRepository.AddTask(taskModel);
            var taskRes = TaskDto.FromTask(task);
            return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<TaskDto>()
            {
                Data = taskRes,
                Success = true,
                Message = "Task created successfully"
            });
        }
    }
}
