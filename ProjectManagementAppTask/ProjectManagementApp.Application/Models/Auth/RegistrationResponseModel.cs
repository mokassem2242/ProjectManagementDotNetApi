using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Models.Auth
{
    public class RegistrationResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
