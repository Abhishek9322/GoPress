using GoPress.Domain.Entities;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);

        Task UpdateAsync(RefreshToken refreshToken);    
    }

}
