using ProjectManagementApp.Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Contracts.Infrastructure.identity
{
    public interface IAuthService
    {
        //login
        Task<AuthModel> TokenGeneratorAsync(TokenRequestModel model);

        //signup
        Task<RegistrationResponseModel> RegisterAsync(ResgisterUserModel model);
        Task<string> RoleAssignment(RoleAssignmentModel model);
    }
}
