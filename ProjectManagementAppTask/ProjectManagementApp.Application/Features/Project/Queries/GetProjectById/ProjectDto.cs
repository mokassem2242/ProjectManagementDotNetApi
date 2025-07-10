using System;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetProjectById
{
    public class ProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        // Optionally, add User or MainTasks if needed as DTOs
    }
} 