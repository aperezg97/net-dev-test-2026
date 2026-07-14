using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries.Tasks
{
    public record GetAllTasksByUserQuery(Guid userId) : IRequest<BaseResponseDto<List<TaskDto>>>;
    public class GetAllTasksByUserQueryHandler : IRequestHandler<GetAllTasksByUserQuery, BaseResponseDto<List<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        public GetAllTasksByUserQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<BaseResponseDto<List<TaskDto>>> Handle(GetAllTasksByUserQuery request, CancellationToken cancellationToken)
        {
            var result = _taskRepository.GetTasksByUserId(request.userId);
            return Task.FromResult(new BaseResponseDto<List<TaskDto>>
            {
                Data = result.Select(x => TaskDto.FromTask(x)).ToList(),
                Success = true,
            });
        }
    }
}
