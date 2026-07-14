using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Commands.Users
{
    public record CreateUserCommand(CreateUserDto userData) : IRequest<BaseResponseDto<UserDto>>;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponseDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<BaseResponseDto<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userModel = CreateUserDto.FromCreateUserDto(request.userData);
            var user = _userRepository.AddUser(userModel);
            var userRes = UserDto.FromUser(user);
            userRes.Password = null; // Do not return the password in the response
            return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<UserDto>()
            {
                Data = userRes,
                Success = true,
                Message = "User created successfully"
            });
        }
    }
}
