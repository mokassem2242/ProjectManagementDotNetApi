using MediatR;
using FluentValidation;
using System;
using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Application.Features.ProjectTask.Queries.GetProjectTaskById
{
    public class GetProjectTaskByIdQuery : IRequest<GetProjectTaskByIdResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectTaskByIdQueryValidator : AbstractValidator<GetProjectTaskByIdQuery>
    {
        public GetProjectTaskByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Task Id is required.");
        }
    }

    public class GetProjectTaskByIdResponse : ProjectManagementApp.Application.Responses.BaseResponse
    {
        public GetAllProjectTasks.GetAllProjectTasksDto Task { get; set; }
    }

    public class GetProjectTaskByIdQueryHandler : IRequestHandler<GetProjectTaskByIdQuery, GetProjectTaskByIdResponse>
    {
        private readonly Contracts.Infrastructure.Persistence.IProjectTaskRepository _projectTaskRepository;

        public GetProjectTaskByIdQueryHandler(Contracts.Infrastructure.Persistence.IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<GetProjectTaskByIdResponse> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _projectTaskRepository.GetByIdAsync(request.Id);
            var response = new GetProjectTaskByIdResponse();
            if (task == null)
            {
                response.Success = false;
                response.Message = $"Task with Id {request.Id} not found.";
                return response;
            }
            response.Task = new GetAllProjectTasks.GetAllProjectTasksDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId
            };
            response.Success = true;
            response.Message = "Task loaded successfully.";
            return response;
        }
    }
} 