using MediatR;
using FluentValidation;

namespace ProjectManagementApp.Application.Features.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<CreateProjectCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("MainProject name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("MainProject description is required.");
            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Due date is required.");
        }
    }
}