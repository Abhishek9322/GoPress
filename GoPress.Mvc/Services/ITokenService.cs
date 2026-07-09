namespace GoPress.Mvc.Services
{
    public interface ITokenService
    {
        void SaveToken(string token);

        void RemoveToken();
    }
}
