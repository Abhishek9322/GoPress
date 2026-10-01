using GoPress.Application.DTOs.Admin.UserManegment;
using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IAdminUserManegementRepository
    {
        Task<List<ApplicationUser>> GetAllCustomerProfile();
    }
}
