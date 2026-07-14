using TaskApp.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Business.Commands.Tasks;
using TaskApp.Business.Queries.Tasks;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto data, ISender mediator)
        {
            var command = new CreateTaskCommand(data);
            BaseResponseDto<TaskDto> result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskDto data, ISender mediator)
        {
            var command = new UpdateTaskCommand(data);
            BaseResponseDto<TaskDto> result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ISender mediator, [FromQuery] bool onlyActives = true)
        {
            var query = new GetAllTasksQuery(onlyActives);
            BaseResponseDto<List<TaskDto>> result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("/by-status/{statusId}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid statusId, ISender mediator)
        {
            var query = new GetAllTasksByStatusQuery(statusId);
            BaseResponseDto<List<TaskDto>> result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("/by-user/{userId}")]
        public async Task<IActionResult> GetAllByUser([FromRoute] Guid userId, ISender mediator)
        {
            var query = new GetAllTasksByUserQuery(userId);
            BaseResponseDto<List<TaskDto>> result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
