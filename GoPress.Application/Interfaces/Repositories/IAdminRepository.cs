using GoPress.Application.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<AdminDashboardDto> GetDashboardAsync();
    }
}
