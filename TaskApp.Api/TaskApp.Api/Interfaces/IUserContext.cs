namespace TaskApp.Interfaces
{
    public interface IUserContext
    {
        public Guid UserId { get; }
        public string Email { get; }
        public bool IsAuthenticated { get; }
    }
}
