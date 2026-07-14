using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries
{
    public record LoginQuery(LoginDto login) : IRequest<BaseResponseDto<UserDto>>;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, BaseResponseDto<UserDto>>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IPasswordHelper passwordHelper, IUserRepository userRepository)
        {
            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
            // _passwordHelper = new PasswordHelper(new PasswordHasher<User>());
        }

        public Task<BaseResponseDto<UserDto>> Handle(LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(loginQuery.login.User) || string.IsNullOrEmpty(loginQuery.login.Pass))
            {
                return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<UserDto>()
                {
                    Success = false,
                    Message = "Username and password are required",
                });
            }
            var userMatched = _userRepository.GetUserByUsername(loginQuery.login.User);
            if (userMatched == null)
            {
                return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<UserDto>()
                {
                    Success = false,
                    Message = "User not found",
                });
            }

            var result = _passwordHelper.VerifyPassword(userMatched, userMatched.Password, loginQuery.login.Pass);
            // var pass = _passwordHelper.HashPassword(userMatched, userMatched.Password);
            var message = (result ? "Login successful" : "Invalid username or password");
            return System.Threading.Tasks.Task.FromResult(new BaseResponseDto<UserDto>()
            {
                Data = UserDto.FromUser(userMatched),
                Success = result,
                Message = message,
            });
        }
    }
}
