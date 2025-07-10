using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Application.Responses;
using FluentValidation;
using System.Collections.Generic;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.Project.Commands.DeleteProject
{
    public class DeleteProjectCommandResponse : BaseResponse
    {
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProjectCommandValidator();
            var response = new DeleteProjectCommandResponse();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationCustomException(validationResult);
            }

            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project == null)
            {
                response.Success = false;
                response.Message = "Project not found.";
                return response;
            }

            await _projectRepository.DeleteAsync(project);
            response.Success = true;
            return response;
        }
    }
}