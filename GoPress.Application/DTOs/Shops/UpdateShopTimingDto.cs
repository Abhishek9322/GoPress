using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.DTOs.Shops
{
    public class UpdateShopTimingDto
    {
        [Required]
        public TimeOnly OpningTime { get; set; }

        [Required]
        public TimeOnly ClosingTime { get; set; }
    }
}
