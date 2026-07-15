namespace GoPress.Mvc.Services
{
    public interface ITokenService
    {
        void SaveToken(string token);
        string? GetToken();
        void RemoveToken();
    }
}
