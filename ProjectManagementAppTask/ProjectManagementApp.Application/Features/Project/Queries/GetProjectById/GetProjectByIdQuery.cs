using System;
using MediatR;
using FluentValidation;
using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<GetProjectByIdResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("MainProject Id is required.");
        }
    }
}