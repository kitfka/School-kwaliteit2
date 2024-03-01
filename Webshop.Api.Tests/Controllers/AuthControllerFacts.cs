using Microsoft.AspNetCore.Mvc;
using Webshop.Api.Controllers;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Api.Tests.Controllers
{
    public class AuthControllerFacts
    {
        private static AuthController GetAuthController() => new AuthController(new MockAuthService());
        private readonly string DefaultEmail = "email@example.com";
        //private readonly string DefaultUserName = "Test";
        private readonly string DefaultPassword = "password";
        private readonly string NewEmail = "otherEmail@example.com";
        private readonly string NewUserName = "newTest";
        private readonly string NewPassword = "newPassword";

        #region Login
        [Fact]
        public void Login_Should_ReturnNotNull()
        {
            // Arrange
            AuthController controller = GetAuthController();

            // Act
            var actionResult = controller.Login(new LoginUser() { Email = "email@example.com", Password = "password" });

            // Assert
            _ = Assert.IsType<OkObjectResult>(actionResult);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("email@example.com", "")]
        [InlineData("", "password")]
        public void Login_WithMissingFields_ReturnBadRequest(string email, string password)
        {
            // Arrange
            AuthController controller = GetAuthController();
            LoginUser login = new() { Email = email, Password = password };

            // Act
            var actionResult = controller.Login(login);

            // Assert
            _ = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void Login_WithCorrectFields_DoNotReturnUserPassword()
        {
            // Arrange
            AuthController controller = GetAuthController();
            LoginUser login = new() { Email = DefaultEmail, Password = DefaultPassword};

            // Act
            var actionResult = controller.Login(login);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<ApplicationUser>(objectResult.Value);

            Assert.True(string.IsNullOrEmpty(result.Password));
        }

        [Fact]
        public void Login_WithCorrectFields_ReturnToken()
        {
            // Arrange
            AuthController controller = GetAuthController();
            LoginUser login = new() { Email = DefaultEmail, Password = DefaultPassword };

            // Act
            var actionResult = controller.Login(login);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<ApplicationUser>(objectResult.Value);

            Assert.False(string.IsNullOrEmpty(result.Token));
        }
        #endregion

        #region Register
        [Fact]
        public void Register_Correct_OkObjectResult()
        {
            // Arrange
            AuthController controller = GetAuthController();
            RegisterUser registerUser = new() { Name = NewUserName, Email = NewEmail, Password = NewPassword };

            // Act
            var actionResult = controller.Register(registerUser);

            // Assert
            _ = Assert.IsType<OkObjectResult>(actionResult);
        }

        [Theory]
        [InlineData("", "newEmail@example.com", "newPassword")]
        [InlineData("newUser", "", "newPassword")]
        [InlineData("newUser", "newEmail@example.com", "")]
        //[InlineData("newUser", "newEmail@example.com", "123")] // this would fail when running full server. However calling controller like this it it won't.
        public void Register_WithBadFields_ReturnBadRequestObjectResult(string userName, string email, string password)
        {
            // Arrange
            AuthController controller = GetAuthController();
            RegisterUser registerUser = new() { Name=userName, Email = email, Password = password };

            // Act
            var actionResult = controller.Register(registerUser);

            // Assert
            _ = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void Register_AsExistingUser_ReturnBadRequestObjectResult()
        {
            // Arrange
            AuthController controller = GetAuthController();
            RegisterUser registerUser = new() { Name = NewUserName, Email = NewEmail, Password = NewPassword };

            // Act
            _ = controller.Register(registerUser);
            var actionResult = controller.Register(registerUser);

            // Assert
            _ = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        #endregion
    }
}
