using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task AddUserAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task SaveChangesAsync();
        Task<ApplicationUser?> GetByIdAsync(int id);
    }

}
