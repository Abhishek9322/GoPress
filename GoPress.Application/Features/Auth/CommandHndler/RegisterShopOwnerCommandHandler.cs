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
    public class RegisterShopOwnerCommandHandler : IRequestHandler<RegisterShopOwnerCommand, AuthResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _password;
        public RegisterShopOwnerCommandHandler(IUserRepository repository, IPasswordHasher password)
        {
            _password = password;
            _repository = repository;

        }
        public async Task<AuthResponse> Handle(RegisterShopOwnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterShopOwnerDto;
            // Check Email Exists
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

            // Create Shop Owner User
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash =_password.HashPassword(dto.Password),
                Role = UserRoleenum.ShopOwner,
                IsApproved = false,    // IMPORTANT
                IsActive = false,

                ShopOwnerProfile = new ShopOwnerProfile
                {
                    ShopName = dto.ShopName,
                    ShopAddress = dto.ShopAddress,
                    City = dto.City,
                    State = dto.State,
                    Pincode = dto.Pincode,
                    ShopLicenseNumber=dto.ShopLicenseNumber,
                    GSTNumber = dto.GSTNumber
                }
            };

            await _repository.AddUserAsync(user);

            await _repository.SaveChangesAsync();

            return new AuthResponse
            {
                Success = true,
                Message =
                    "Shop Owner registration submitted successfully. Waiting for admin approval."
            };

        }
    }
}
