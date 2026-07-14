using TaskApp.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Business.Commands.Users;
using TaskApp.Business.Queries;
using TaskApp.Business.Queries.Users;
using TaskApp.Interfaces;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly ITokenProviderSevice _tokenProviderSevice;

        public AuthController(IUserContext userContext, ITokenProviderSevice tokenProviderSevice)
        {
            _tokenProviderSevice = tokenProviderSevice;
            _userContext = userContext;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto login, ISender mediator)
        {
            var query = new LoginQuery(login);
            BaseResponseDto<UserDto> result = await mediator.Send(query);
            if (result.Success && result.Data != null)
            {
                var token = _tokenProviderSevice.GenerateToken(result.Data.Id.ToString(), result!.Data!.Email!);
                return Ok(new BaseResponseDto<LoginResponseDto>
                {
                    Data = new LoginResponseDto() { Token = token, User = result.Data },
                    Success = true
                });
            }
            return Ok(new BaseResponseDto<LoginResponseDto>()
            {
                Success = false,
                Message = result.Message ?? "Invalid username or password"
            });
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] CreateUserDto createUserDto, ISender mediator)
        {
            var command = new CreateUserCommand(createUserDto);
            BaseResponseDto<UserDto> result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile(ISender mediator)
        {
            var userId = _userContext.UserId;
            var query = new GetUserByIdQuery(userId);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
