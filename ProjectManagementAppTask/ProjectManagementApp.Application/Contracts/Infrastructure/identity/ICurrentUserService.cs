using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Contracts.Infrastructure.identity
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal GetCurrentUser();
        string GetUserId();
    }
}
