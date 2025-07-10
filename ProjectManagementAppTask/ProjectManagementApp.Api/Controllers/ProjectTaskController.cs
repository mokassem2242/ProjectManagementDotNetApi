using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Application.Features.ProjectTask.Commands.CreateProjectTask;
using ProjectManagementApp.Application.Features.ProjectTask.Commands.UpdateProjectTask;
using ProjectManagementApp.Application.Features.ProjectTask.Commands.DeleteProjectTask;
using ProjectManagementApp.Application.Features.ProjectTask.Queries.GetProjectTaskById;
using ProjectManagementApp.Application.Features.ProjectTask.Queries.GetAllProjectTasks;


namespace ProjectManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = taskId }, taskId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetProjectTaskByIdQuery { Id = id });
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _mediator.Send(new GetAllProjectTasksQuery());
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProjectTaskCommand { Id = id });
            return NoContent();
        }
    }   
} 