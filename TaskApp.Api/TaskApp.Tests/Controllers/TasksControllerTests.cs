using TaskApp.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskApp.Business.Commands.Tasks;
using TaskApp.Controllers;

namespace TaskApp.Tests.Controllers
{
    public class TasksControllerTests
    {
        [Fact]
        public async Task TasksController_ShouldCreateTaskAsync()
        {
            var taskParam = new TaskDto
            {
                Name = "Test Task",
                Description = "Test Description",
                DueDate = DateTime.Now,
                AssignedToId = Guid.NewGuid(),
                StatusId = Guid.NewGuid(),
            };
            var expectedTask = new TaskDto
            {
                Id = Guid.NewGuid(),
                Name = taskParam.Name,
                Description = taskParam.Description,
                DueDate = taskParam.DueDate,
                AssignedToId = taskParam.AssignedToId,
                StatusId = taskParam.StatusId,
            };
            var expectedResponse = new BaseResponseDto<TaskDto>
            {
                Data = expectedTask,
                Success = true,
                Message = "Task created successfully"
            };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

            var controller = new TaskController();
            var result = await controller.Create(taskParam, mediatorMock.Object);

            Assert.NotNull(result);
            var innerResult = (result! as OkObjectResult)!.Value as BaseResponseDto<TaskDto>;
            Assert.True(innerResult!.Success);
            Assert.NotEmpty(innerResult!.Data!.Id.ToString());
        }
    }
}
