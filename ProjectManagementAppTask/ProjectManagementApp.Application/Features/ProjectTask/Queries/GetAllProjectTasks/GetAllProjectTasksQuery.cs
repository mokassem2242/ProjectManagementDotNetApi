using MediatR;
using System.Collections.Generic;

namespace ProjectManagementApp.Application.Features.ProjectTask.Queries.GetAllProjectTasks
{
    public class GetAllProjectTasksDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class GetAllProjectTasksResponse : ProjectManagementApp.Application.Responses.BaseResponse
    {
        public IReadOnlyList<GetAllProjectTasksDto> Tasks { get; set; }
    }

    public class GetAllProjectTasksQuery : IRequest<GetAllProjectTasksResponse>
    {
    }

    public class GetAllProjectTasksQueryHandler : IRequestHandler<GetAllProjectTasksQuery, GetAllProjectTasksResponse>
    {
        private readonly Contracts.Infrastructure.Persistence.IProjectTaskRepository _projectTaskRepository;

        public GetAllProjectTasksQueryHandler(Contracts.Infrastructure.Persistence.IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<GetAllProjectTasksResponse> Handle(GetAllProjectTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _projectTaskRepository.ListAllAsync();
            var response = new GetAllProjectTasksResponse
            {
                Tasks = tasks.Select(task => new GetAllProjectTasksDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                    DueDate = task.DueDate,
                    ProjectId = task.ProjectId
                }).ToList()
            };
            return response;
        }
    }
} 