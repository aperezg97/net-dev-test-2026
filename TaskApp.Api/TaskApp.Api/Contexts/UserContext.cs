using Microsoft.IdentityModel.JsonWebTokens;
using TaskApp.Interfaces;

namespace TaskApp.Contexts
{
    public sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get => Guid.Parse(
            _httpContextAccessor.HttpContext?.User
                .FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException());
        }

        public string Email
        {
            get => _httpContextAccessor.HttpContext?.User
                .FindFirst(JwtRegisteredClaimNames.Email)?.Value
            ?? throw new UnauthorizedAccessException();
        }
        public bool IsAuthenticated { get => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false; }
    }
}
