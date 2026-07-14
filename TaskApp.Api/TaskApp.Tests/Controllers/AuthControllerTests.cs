using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskApp.Business.Queries;
using TaskApp.Controllers;
using TaskApp.Entities.Dtos;
using TaskApp.Interfaces;

namespace TaskApp.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task AuthController_ShouldDoLoginAsync()
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
            var mockUserContext = new Mock<IUserContext>();
            var mockTokenProviderService = new Mock<ITokenProviderSevice>();
            mockTokenProviderService
                .Setup(m => m.GenerateToken(It.Is<string>(x => x == user.Id.ToString()), It.Is<string>(x => x == user.Email)))
                .Returns("hash-password");
            var mockSender = new Mock<ISender>();
            mockSender.Setup(m => m.Send(
                It.IsAny<LoginQuery>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new BaseResponseDto<UserDto> { Success = true, Data = user });


            var controller = new AuthController(mockUserContext.Object, mockTokenProviderService.Object);
            var loginDto = new LoginDto()
            {
                User = "testuser",
                Pass = "testpass",
            };
            var result = await controller.Login(loginDto, mockSender.Object);


            Assert.NotNull(result);
            var innerResult = (result! as OkObjectResult)!.Value as BaseResponseDto<LoginResponseDto>;
            Assert.True(innerResult!.Success);
            Assert.NotEmpty(innerResult!.Data!.Token);
        }
    }
}
