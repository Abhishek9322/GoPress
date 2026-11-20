using GoPress.Domain.Common;
using GoPress.Domain.Enums;

namespace GoPress.Domain.Entities
{
    public class SupportTicket : BaseEntity
    {
        public string TicketNumber { get; set; }

        // Order Information
        public int OrderId { get; set; }

        // Ticket Information
        public string Subject { get; set; }

        public string Description { get; set; }

        public SupportCategoryEnum Category { get; set; }

        public SupportTicketStatusEnum Status { get; set; }= SupportTicketStatusEnum.Open;

        public SupportPriorityEnum Priority { get; set; }= SupportPriorityEnum.Medium;

        // Assigned Admin
        public int? AssignedAdminId { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        // Navigation Properties

        public Order Order { get; set; }

        public ApplicationUser AssignedAdmin { get; set; }

        public ICollection<SupportMessage> Messages { get; set; } = new List<SupportMessage>();
    }
}
