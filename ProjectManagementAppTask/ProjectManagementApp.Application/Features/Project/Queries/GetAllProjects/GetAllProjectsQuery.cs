using System.Collections.Generic;
using MediatR;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<GetAllProjectResponse>
    {
    }

    public class GetAllProjectsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public List<ProjectTaskDto> Tasks { get; set; }
    }

    public class ProjectTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        // Add other properties as needed, e.g., public Guid Id { get; set; }
    }
}