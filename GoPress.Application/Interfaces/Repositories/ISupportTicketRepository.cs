using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Interfaces.Repositories
{
    public interface ISupportTicketRepository
    {
        Task<SupportTicket> CreateSupportTicketAsync(SupportTicket supportTicket);

        Task<SupportTicket?> GetSupportTicketByIdAsync(int ticketId);

        Task<List<SupportTicket>> GetByOrderIdAsync(int orderId);

        Task<List<SupportTicket>> GetByCustomerIdAsync(int customerId);

        Task<List<SupportTicket>> GetAllSupportTicketAsync();

        Task<List<SupportTicket>> GetByStatusAsyns(SupportTicketStatusEnum status);

        Task UpdateAsync(SupportTicket ticket);

    }
}
