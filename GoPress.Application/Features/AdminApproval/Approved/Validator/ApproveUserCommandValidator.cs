using FluentValidation;
using GoPress.Application.Features.AdminApproval.Approved.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.AdminApproval.Approved.Validator
{
    public class ApproveUserCommandValidator:AbstractValidator<ApproveUserCommand>
    {
        public ApproveUserCommandValidator()
        {
            RuleFor(x=>x.UserId)
               .GreaterThan(0)
               .WithMessage("UserId must be greater than 0");
        }
    }
}
