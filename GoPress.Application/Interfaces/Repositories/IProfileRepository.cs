using GoPress.Application.DTOs.Profile;
using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<ApplicationUser?> GetCustomerProfileAsync(int userId);

        Task<ApplicationUser?> GetShopOwnerProfileAsync(int userId);

        Task<ApplicationUser?> GetDeliveryBoyProfileAsync(int userId);
        Task<ApplicationUser?> GetByIdAsync(int id);
        Task updateAsync(ApplicationUser user);
    }
}
