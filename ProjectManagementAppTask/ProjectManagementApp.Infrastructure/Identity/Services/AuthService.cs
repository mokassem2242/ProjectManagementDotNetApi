using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using ProjectManagementApp.Application.Models.Auth;
using ProjectManagementApp.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Infrastructure.Persistence.Repositories;

namespace ProjectManagementApp.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDomainUserRepository _domainUserRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        public RoleManager<IdentityRole<Guid>> _roleManager { get; }

        public AuthService(
            UserManager<ApplicationUser> userManager,
             IDomainUserRepository domainUserRepository,
            IOptions<JwtOptions> jwtOptions,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _domainUserRepository = domainUserRepository;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<RegistrationResponseModel> RegisterAsync(ResgisterUserModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FristName = model.FristName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            var response = new RegistrationResponseModel
            {
                IsSuccessful = result.Succeeded,
                Email = model.Email,
                UserId = user.Id.ToString(),
                Message = result.Succeeded ? "Registration successful" : string.Join(", ", result.Errors.Select(e => e.Description))
            };

            if (result.Succeeded)
            {
                var domainUser = new DomainUser
                {
                    Id = Guid.NewGuid(), // assuming you're generating ID manually
                    ApplicationUserId = user.Id
                };

                await _domainUserRepository.AddAsync(domainUser);
               
            }

            if (result.Succeeded && !string.IsNullOrEmpty(model.role))
            {
                if (!await _roleManager.RoleExistsAsync(model.role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(model.role));
                }

                await _userManager.AddToRoleAsync(user, model.role);
            }

            return response;
        }


        public async Task<string> RoleAssignment(RoleAssignmentModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return "User not found.";

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(model.RoleName));
            }
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            return result.Succeeded ? "Role assigned successfully." : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<AuthModel> TokenGeneratorAsync(TokenRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthModel { IsAuthenticated = false, Message = "Invalid credentials." };
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.DurationInDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new AuthModel
            {
                Id = user.Id.ToString(),
                FristName = user.FristName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsAuthenticated = true,
                Email = user.Email,
                Roles = userRoles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Message = "Authentication successful"
            };
        }
    }
}
