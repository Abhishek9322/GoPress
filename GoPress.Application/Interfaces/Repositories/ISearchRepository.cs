using GoPress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        Task<List<ApplicationUser>> SearchShopsAsync(string keyword);
    }
}
