using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.Features.Project.Queries.GetProjectById;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetProjectByIdResponse();
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null) return null;
            var dto = new ProjectDto
            {
                Name = project.Name,
                Description = project.Description,
                UserId = project.UserId
            };

            response.Message = "project is loaded successfully";
            response.Project = dto;
            return response;
        }
    }
}