using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using System;
using ProjectManagementApp.Application.Responses;
using System.Collections.Generic;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommandResponse : BaseResponse
    {
        public Guid ProjectTaskId { get; set; }
    }

    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, DeleteProjectTaskCommandResponse>
    {
        private readonly IProjectTaskRepository _projectTaskRepository;

        public DeleteProjectTaskCommandHandler(IProjectTaskRepository projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<DeleteProjectTaskCommandResponse> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProjectTaskCommandValidator();
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

            await _projectTaskRepository.DeleteAsync(task);
            var response = new DeleteProjectTaskCommandResponse
            {
                ProjectTaskId = task.Id,
                Success = true,
                Message = "Task deleted successfully."
            };
            return response;
        }
    }
} 