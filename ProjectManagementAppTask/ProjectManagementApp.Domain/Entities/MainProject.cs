using ProjectManagementApp.Domain.Common;
using System;
using System.Collections.Generic;

namespace ProjectManagementApp.Domain.Entities
{
    public class MainProject: AuditableEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public DomainUser User { get; set; }
        public ICollection<ProjectTask> MainTasks { get; set; }
    }
} 