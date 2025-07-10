using MediatR;
using FluentValidation;
using System;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.DeleteProjectTask
{
    public class DeleteProjectTaskCommand : IRequest<DeleteProjectTaskCommandResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectTaskCommandValidator : AbstractValidator<DeleteProjectTaskCommand>
    {
        public DeleteProjectTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Task Id is required.");
        }
    }
} 