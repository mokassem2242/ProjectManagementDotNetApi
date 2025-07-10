using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Models.Auth
{
    public class ResgisterUserModel
    {
        public string Email { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsBanned { get; set; }

        public string role { get; set; }
    }
}
