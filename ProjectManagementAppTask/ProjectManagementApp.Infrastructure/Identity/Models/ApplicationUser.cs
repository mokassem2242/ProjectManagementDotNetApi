using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Infrastructure.Identity.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? FristName { get; set; }
        public string? LastName { get; set; }
        //public DomainUser DomainUser { set; get; }
        public Guid? DomainUserId { get; set; } 

    }
}
