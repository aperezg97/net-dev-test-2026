using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries.Tasks
{
    public record GetAllTasksByStatusQuery(Guid statusId) : IRequest<BaseResponseDto<List<TaskDto>>>;
    public class GetAllTasksByStatusQueryHandler : IRequestHandler<GetAllTasksByStatusQuery, BaseResponseDto<List<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        public GetAllTasksByStatusQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<BaseResponseDto<List<TaskDto>>> Handle(GetAllTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var result = _taskRepository.GetTasksByStatusId(request.statusId);
            return Task.FromResult(new BaseResponseDto<List<TaskDto>>
            {
                Data = result.Select(x => TaskDto.FromTask(x)).ToList(),
                Success = true,
            });
        }
    }
}
