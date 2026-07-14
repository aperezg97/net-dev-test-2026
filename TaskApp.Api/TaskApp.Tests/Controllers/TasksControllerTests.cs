using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Tests.Controllers
{
    public class TasksControllerTests
    {
        [Fact]
        public void TasksController_ShouldCreateTask()
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
            
            var controller = new TaskController();
            var result = controller.Create(taskParam);

            Assert.NotNull(result);
            var innerResult = (result.Result as OkObjectResult).Value as BaseResponseDto<TaskDto>;
            Assert.Equal(System.Threading.Tasks.Task.CompletedTask.Status, result.Status);
            Assert.True(innerResult!.Success);
            Assert.NotEmpty(innerResult!.Data!.Id.ToString());
        }
    }
}
