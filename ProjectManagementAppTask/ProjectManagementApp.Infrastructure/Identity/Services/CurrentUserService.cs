using Microsoft.AspNetCore.Http;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Infrastructure.Identity.Services
{
    public class CurrentUserService:ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

       

        public  ClaimsPrincipal GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User;
        }
        public  string GetUserId()
        {
            // 1) Pull the ClaimsPrincipal
            var user = _httpContextAccessor.HttpContext?.User
                       ?? throw new InvalidOperationException("No HTTP context or user.");

            
            var rawId = user.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? throw new InvalidOperationException("NameIdentifier claim is missing.");



          
            return rawId;
        }
    }
}
