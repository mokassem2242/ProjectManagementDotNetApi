using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Application.Features.Project;
using ProjectManagementApp.Application.Features.Project.Queries;
using System;
using ProjectManagementApp.Application.Features.Project.Commands.CreateProject;
using ProjectManagementApp.Application.Features.Project.Commands.UpdateProject;
using ProjectManagementApp.Application.Features.Project.Commands.DeleteProject;
using ProjectManagementApp.Application.Features.Project.Queries.GetAllProjects;
using ProjectManagementApp.Application.Features.Project.Queries.GetProjectById;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = result.ProjectId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(projects);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProjectCommand { Id = id });
            return NoContent();
        }
    }
} 