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
    public class RegisterDeliveryBoyCommandHandler : IRequestHandler<RegisterDeliveryBoyCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _password;
        public RegisterDeliveryBoyCommandHandler(IUserRepository repository,IPasswordHasher password)
        {
            _password = password;
            _repository = repository;   
        }
        public async Task<AuthResponse> Handle(RegisterDeliveryBoyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterDeliveryBoyDto;

            var isEmailExists = await _repository
                .IsEmailExistsAsync(dto.Email);

            if (isEmailExists)
            {

                return new AuthResponse
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = _password.HashPassword(dto.Password),
                Role = UserRoleenum.DeliveryBoy,
                IsApproved = false,    // IMPORTANT
                IsActive = false,

                DeliveryBoyProfile = new DeliveryBoyProfile
                {
                    BikeNumber=dto.BikeNumber,
                    AadhaarNumber = dto.AadhaarNumber,
                    LicenseNumber = dto.LicenseNumber,
                    Address=dto.Address,
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
                Message = "Registration successful. Please wait for admin approval."
            };

        }
    }
}
