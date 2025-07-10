using System;
using MediatR;
using FluentValidation;

namespace ProjectManagementApp.Application.Features.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<DeleteProjectCommandResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("MainProject Id is required.");
        }
    }
}