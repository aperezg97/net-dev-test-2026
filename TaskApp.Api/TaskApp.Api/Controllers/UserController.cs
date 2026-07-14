using TaskApp.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Business.Queries.Users;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(ISender mediator)
        {
            var query = new GetAllUsersQuery();
            BaseResponseDto<List<UserDto>> result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
