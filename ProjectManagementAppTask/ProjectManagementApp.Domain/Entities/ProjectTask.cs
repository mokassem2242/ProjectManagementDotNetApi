using ProjectManagementApp.Domain.Common;
using System;

namespace ProjectManagementApp.Domain.Entities
{
    public class ProjectTask: AuditableEntity
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public MainProject Project { get; set; }
    }
} 