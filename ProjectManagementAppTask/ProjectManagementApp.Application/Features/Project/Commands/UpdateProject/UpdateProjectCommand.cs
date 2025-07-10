using System;
using MediatR;
using FluentValidation;

namespace ProjectManagementApp.Application.Features.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<UpdateProjectCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("MainProject Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("MainProject name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("MainProject description is required.");
        }
    }
}