using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using ProjectManagementApp.Application.Responses;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskCommandResponse : BaseResponse
    {
        public Guid ProjectTaskId { get; set; }
    }

    public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, UpdateProjectTaskCommandResponse>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public UpdateProjectTaskCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<UpdateProjectTaskCommandResponse> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectTaskCommandValidator();
            var response = new UpdateProjectTaskCommandResponse();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationCustomException(validationResult);
            }

            var task = await _projectTaskRepository.GetByIdAsync(request.Id);
            if (task == null)
            {
                throw new NotFoundException("ProjectTask", request.Id);
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.IsCompleted = request.IsCompleted;
            task.ProjectId = request.ProjectId;

            await _projectTaskRepository.UpdateAsync(task);
            response.ProjectTaskId = task.Id;
            response.Success = true;
            response.Message = "Task updated successfully.";
            return response;
        }
    }
} 