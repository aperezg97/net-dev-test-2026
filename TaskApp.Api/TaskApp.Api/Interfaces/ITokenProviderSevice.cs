namespace TaskApp.Interfaces
{
    public interface ITokenProviderSevice
    {
        public string GenerateToken(string userId, string email);
    }
}
