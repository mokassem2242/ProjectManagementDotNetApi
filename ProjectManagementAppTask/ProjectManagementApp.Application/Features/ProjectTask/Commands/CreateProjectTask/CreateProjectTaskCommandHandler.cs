using System.Collections.Generic;
using ProjectManagementApp.Application.Responses;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using System;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommandResponse : BaseResponse
    {
        public Guid ProjectTaskId { get; set; }
    }

    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand, CreateProjectTaskCommandResponse>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateProjectTaskCommandHandler(IProjectTaskRepository projectTaskRepository, IProjectRepository projectRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<CreateProjectTaskCommandResponse> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProjectTaskCommandValidator();
            var response = new CreateProjectTaskCommandResponse();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationCustomException(validationResult);
            }

            // Fetch parent project and check due date
            var parentProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (parentProject == null)
            {
                throw new NotFoundException("Project", request.ProjectId);
            }
            if (parentProject.DueDate < DateTime.UtcNow)
            {
                throw new BadRequestException("Cannot add a task to a project whose due date has expired.");
            }

            var task = new Domain.Entities.ProjectTask
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                ProjectId = request.ProjectId
            };

            await _projectTaskRepository.AddAsync(task);
            response.ProjectTaskId = task.Id;
            return response;
        }
    }
} 