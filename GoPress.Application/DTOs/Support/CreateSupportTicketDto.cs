using GoPress.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Support
{
    public class CreateSupportTicketDto
    {
        public int OrderId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public SupportCategoryEnum Category { get; set; }
    }
}
