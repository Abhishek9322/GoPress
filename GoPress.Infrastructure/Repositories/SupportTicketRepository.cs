using GoPress.Application.Interfaces.Repositories;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using GoPress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Infrastructure.Repositories
{
    public class SupportTicketRepository : ISupportTicketRepository
    {
        private readonly ApplicationDbContext _context; 
        public SupportTicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SupportTicket> CreateSupportTicketAsync(SupportTicket supportTicket)
        {
            await _context.SupportTickets.AddAsync(supportTicket);
            await _context.SaveChangesAsync();
            return supportTicket;
        }

        public async Task<List<SupportTicket>> GetAllSupportTicketAsync()
        {
            return await _context.SupportTickets
                 .Include(t => t.Order)
                 .ThenInclude(t => t.Customer)
                 .Include(t => t.AssignedAdmin)
                 .Where(t => !t.IsDeleted)
                 .OrderByDescending(t => t.CreatedAt)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<List<SupportTicket>> GetByCustomerIdAsync(int customerId)
        {
           return await _context.SupportTickets
                .Include(t => t.Order)
                .ThenInclude(t => t.Customer)
                .Where(t=>t.Order.CustomerId==customerId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<SupportTicket>> GetByOrderIdAsync(int orderId)
        {
            return await _context.SupportTickets
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<SupportTicket>> GetByStatusAsyns(SupportTicketStatusEnum status)
        {
            return await _context.SupportTickets
                .Include(t=>t.Order)
                .ThenInclude(t=>t.Customer)
                .Include(t=>t.AssignedAdmin)
                .Where(t=>t.Status==status && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order> GetCustomerOrderForSupportAsync(int orderId, int customerId)
        {
            return await _context.Orders
                 .Include(t => t.Customer)
                 .Where(t => t.Id == orderId && t.CustomerId == customerId && !t.IsDeleted)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
        }

        public async Task<SupportTicket?> GetCustomerTicketByIdAsync(int customerId, int ticketId)
        {
            return await _context.SupportTickets
                .Include(x => x.Order)
                .Include(x => x.AssignedAdmin)
                .Where(x =>
                           x.Id == ticketId &&
                           x.Order.CustomerId == customerId &&
                          !x.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<SupportTicket?> GetSupportTicketByIdAsync(int ticketId)
        {
            return await _context.SupportTickets
                .Include(t => t.Order)
                .Include(t => t.AssignedAdmin)
                .Include(t => t.Messages)
                .ThenInclude(m => m.Sender)
                .Where(t=>! t.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == ticketId && !t.IsDeleted);
        }

        public Task UpdateAsync(SupportTicket ticket)
        {
           _context.SupportTickets.Update(ticket);
              return _context.SaveChangesAsync();
        }
    }
}
