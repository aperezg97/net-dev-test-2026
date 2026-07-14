using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries.Users
{
    public record GetAllUsersQuery() : IRequest<BaseResponseDto<List<UserDto>>>;
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResponseDto<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDto<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUsersAsync();
            return new BaseResponseDto<List<UserDto>>
            {
                Data = result.Select(user => UserDto.FromUser(user)).ToList(),
                Success = true,
            };
        }
    }
}
