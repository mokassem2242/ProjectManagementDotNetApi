using ProjectManagementApp.Domain.Common;
using System.Collections.Generic;

namespace ProjectManagementApp.Domain.Entities
{
    public class DomainUser: AuditableEntity
    {
        public Guid ApplicationUserId { get; set; }

        public ICollection<MainProject> Projects { get; set; } = new List<MainProject>();

    }
} 