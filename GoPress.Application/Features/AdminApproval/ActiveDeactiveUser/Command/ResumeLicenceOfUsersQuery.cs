using AutoMapper.Configuration.Conventions;
using GoPress.Application.Features.Orders.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.ActiveDeactiveUser.Command
{
    public class ResumeLicenceOfUsersQuery:IRequest<Response<string>>
    {
        public int userId { get; set; }
        public bool IsActive { get; set; }
    }
}
