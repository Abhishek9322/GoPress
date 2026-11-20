using GoPress.Domain.Common;

namespace GoPress.Domain.Entities
{
    public class SupportMessage : BaseEntity
    {
        public int SupportTicketId { get; set; }

        // Message Sender
        public int SenderId { get; set; }

        public string Message { get; set; }

        public string? AttachmentUrl { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime? ReadAt { get; set; }

        // Navigation Properties

        public SupportTicket SupportTicket { get; set; }

        public ApplicationUser Sender { get; set; }
    }
}
