using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Domain.Entities;
using System.Linq;


namespace ProjectManagementApp.Application.Features.Project.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery,GetAllProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetAllProjectResponse> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllProjectResponse();
            var projects = await _projectRepository.ListAllWithTasksAsync();
            var dtos = new List<GetAllProjectsDto>();
            foreach (var project in projects)
            {
                dtos.Add(new GetAllProjectsDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    DueDate = project.DueDate,
                    IsCompleted = project.IsCompleted,
                    Tasks = project.MainTasks?.Select(task => new ProjectTaskDto
                    {
                        Id= task.Id,
                        Title = task.Title,
                        Description = task.Description,
                        IsCompleted = task.IsCompleted,
                        DueDate= task.DueDate,

                       
                     
                     
                    }).ToList() ?? new List<ProjectTaskDto>()
                });
            }
            response.Projects= dtos;
            response.Success = true;
            response.Message = "Projects is loaded sucessfully";
            return response;
        }
    }
}