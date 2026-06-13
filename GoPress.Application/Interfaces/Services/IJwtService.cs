using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);

        string GenerateRefreshToken();
    }
}
