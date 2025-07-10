using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectManagementApp.Domain.Entities;

using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using System;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using System.Collections.Generic;
using ProjectManagementApp.Application.Exceptions;

namespace ProjectManagementApp.Application.Features.Project.Commands.CreateProject
{
    public class CreateProjectCommandResponse : ProjectManagementApp.Application.Responses.BaseResponse
    {
        public Guid ProjectId { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
    {
        private readonly IProjectRepository _projectRepository;

        public IDomainUserRepository _domainUserRepository { get; }
        public ICurrentUserService _currentUserService { get; }

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IDomainUserRepository domainUserRepository ,ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _domainUserRepository = domainUserRepository;
            _currentUserService = currentUserService;
        }

      

        public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProjectCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationCustomException(validationResult);
            }
            var identityId = Guid.Parse(_currentUserService.GetUserId());
            var user = await _domainUserRepository.GetByIdentityId(identityId);
            if (user == null)
            {
                throw new NotFoundException("User", identityId);
            }
            var project = new MainProject
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserId = user.Id
            };
            await _projectRepository.AddAsync(project);
            var response = new CreateProjectCommandResponse { ProjectId = project.Id };
            return response;
        }
    }
}