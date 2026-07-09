using GoPress.Mvc.Models.Auth;

namespace GoPress.Mvc.Services
{
    public interface ICurrentUserService
    {
        CurrentUserViewModel? GetCurrentUser();

        string? GetToken();

        bool IsAuthenticated();
    }
}
