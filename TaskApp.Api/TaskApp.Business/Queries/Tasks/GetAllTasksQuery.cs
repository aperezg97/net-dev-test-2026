using TaskApp.Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries.Tasks
{
    public record GetAllTasksQuery(bool activeOnly) : IRequest<BaseResponseDto<List<TaskDto>>>;
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, BaseResponseDto<List<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        public GetAllTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<BaseResponseDto<List<TaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var result = _taskRepository.GetAllTasks(request.activeOnly);
            return Task.FromResult(new BaseResponseDto<List<TaskDto>>
            {
                Data = result.Select(x => TaskDto.FromTask(x)).ToList(),
                Success = true,
            });
        }
    }
}
