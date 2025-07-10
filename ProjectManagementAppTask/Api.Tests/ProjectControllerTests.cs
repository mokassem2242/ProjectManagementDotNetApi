using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Api.Controllers;
using ProjectManagementApp.Application.Features.Project.Commands.CreateProject;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementApp.Application.Features.Project.Commands.UpdateProject;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Tests
{
    public class ProjectControllerTests
    {
        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WithProjectId()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var command = new CreateProjectCommand
            {
                Name = "Mock Project Name",
                Description = "Mock project description for testing.",
                DueDate = DateTime.UtcNow.AddDays(30),
                IsCompleted = false
            };
            var expectedResult = new CreateProjectCommandResponse { ProjectId = Guid.NewGuid() };
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateProjectCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
            var controller = new ProjectController(mediatorMock.Object);

            // Act
            var result = await controller.Create(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var response = Assert.IsType<CreateProjectCommandResponse>(createdResult.Value);
            Assert.Equal(expectedResult.ProjectId, response.ProjectId);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult_WhenIdsMatch()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var projectId = Guid.NewGuid();
            var command = new UpdateProjectCommand
            {
                Id = projectId,
                Name = "Updated Project Name",
                Description = "Updated project description for testing.",
                DueDate = DateTime.UtcNow.AddDays(60),
                IsCompleted = true
            };
            var expectedResult = new UpdateProjectCommandResponse { Success = true };
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProjectCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
            var controller = new ProjectController(mediatorMock.Object);

            // Act
            var result = await controller.Update(projectId, command);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
} 