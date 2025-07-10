using MediatR;
using FluentValidation;
using System;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.CreateProjectTask
{
    public class CreateProjectTaskCommand : IRequest<CreateProjectTaskCommandResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class CreateProjectTaskCommandValidator : AbstractValidator<CreateProjectTaskCommand>
    {
        public CreateProjectTaskCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Task title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Task description is required.");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
        }
    }
} 