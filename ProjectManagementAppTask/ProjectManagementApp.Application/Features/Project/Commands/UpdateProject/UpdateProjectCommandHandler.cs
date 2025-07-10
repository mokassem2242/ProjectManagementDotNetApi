using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.Responses;
using FluentValidation;
using System.Collections.Generic;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandResponse : BaseResponse
    {
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationCustomException(validationResult);
            }
            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
            {
                throw new NotFoundException("Project", request.Id);
            }
            project.Name = request.Name;
            project.Description = request.Description;
            await _projectRepository.UpdateAsync(project);
            var response = new UpdateProjectCommandResponse { Success = true };
            return response;
        }
    }
}