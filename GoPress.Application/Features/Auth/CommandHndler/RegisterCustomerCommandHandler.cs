using GoPress.Application.Features.Auth.Commands;
using GoPress.Application.Features.Auth.Responses;
using GoPress.Application.Interfaces.Repositories;
using GoPress.Application.Interfaces.Services;
using GoPress.Domain.Entities;
using GoPress.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Auth.CommandHndler
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;
        public RegisterCustomerCommandHandler(IUserRepository repository,IPasswordHasher hasher)
        {
            _hasher = hasher;
            _repository = repository;
        }
        public async Task<AuthResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterCustomerDto;

            var isEmailExists = await _repository.IsEmailExistsAsync(dto.Email); //Check Email Exist or not 

            if (isEmailExists)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            //Create user

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = _hasher.HashPassword(dto.Password), //Hash Password
                Role = UserRoleenum.Customer,
                IsApproved = true,
                IsActive = true,

                CustomerProfile = new CustomerProfile
                {
                    Address = dto.Address,
                    City = dto.City,
                    State = dto.State,
                    Pincode = dto.Pincode
                }
            };
            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();

            return new AuthResponse
            {
                Success = true,
                Message = "Customer registered successfully."


            };
        }
    }
}
