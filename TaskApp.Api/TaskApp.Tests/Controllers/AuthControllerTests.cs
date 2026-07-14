using Microsoft.AspNetCore.Mvc;

namespace TaskApp.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public void AuthController_ShouldDoLogin()
        {
            var user = new UserDto()
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Password = "",
                FirstName = "Test",
                LastName = "User",
                Email = ""
            };
            
            var controller = new AuthController();
            var loginDto = new LoginDto()
            {
                User = "testuser",
                Pass = "testpass",
            };
            var result = controller.Login(loginDto);

            Assert.NotNull(result);
            var innerResult = (result.Result as OkObjectResult).Value as LoginResponseDto;
            Assert.Equal(System.Threading.Tasks.Task.CompletedTask.Status, result.Status);
            Assert.True(innerResult!.Success);
        }
    }
}
