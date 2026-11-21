using GoPress.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Support
{
    public class SupportTicketDto
    {
        public int Id { get; set; }

        public string TicketNumber { get; set; }

        public int OrderId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public SupportCategoryEnum Category { get; set; }

        public SupportTicketStatusEnum Status { get; set; }

        public SupportPriorityEnum Priority { get; set; }

        public int? AssignedAdminId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public DateTime? ClosedAt { get; set; }
    }
}
