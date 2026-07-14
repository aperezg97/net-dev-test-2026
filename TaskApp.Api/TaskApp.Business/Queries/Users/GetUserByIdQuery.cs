using TaskApp.Entities.Dtos;
using MediatR;
using TaskApp.Data.Interfaces.Repositories;

namespace TaskApp.Business.Queries.Users
{
    public record GetUserByIdQuery(Guid userId) : IRequest<BaseResponseDto<UserDto>>;
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponseDto<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDto<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByIdAsync(request.userId);
            return new BaseResponseDto<UserDto>
            {
                Data = result != null ? UserDto.FromUser(result) : null,
                Success = result != null,
            };
        }
    }
}
