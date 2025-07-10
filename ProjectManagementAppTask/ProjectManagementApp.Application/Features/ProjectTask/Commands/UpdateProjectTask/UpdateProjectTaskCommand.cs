using MediatR;
using FluentValidation;
using System;

namespace ProjectManagementApp.Application.Features.ProjectTask.Commands.UpdateProjectTask
{
    public class UpdateProjectTaskCommand : IRequest<UpdateProjectTaskCommandResponse>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class UpdateProjectTaskCommandValidator : AbstractValidator<UpdateProjectTaskCommand>
    {
        public UpdateProjectTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Task Id is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Task title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Task description is required.");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
        }
    }
} 