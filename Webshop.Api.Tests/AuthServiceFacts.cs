using Microsoft.Extensions.Configuration;
using Webshop.Model;

namespace Webshop.Api.Tests
{
    public class AuthServiceFacts
    {
        private readonly IConfiguration configuration = new MockConfiguration();
        private static ApplicationUser GetMockUser()
        {
            return new()
            {
                Email = "email@example.com",
                Name = "Test",
                IsActive = true,
                Password = "password",
            };
        }

        private AuthService GetAuthService()
        {
            return new(new LiteDBService(configuration), configuration);
        }

        #region TryLogin
        [Fact]
        public void TryLogin_WithBadCredentials_ShouldReturnFalseAndNull()
        {
            // Arrange
            AuthService authService = GetAuthService();
            ApplicationUser NewUser = GetMockUser();

            // Act
            bool result = authService.TryLogin(NewUser.Email, NewUser.Password, out var user);

            // Assert
            Assert.False(result);
            Assert.Null(user);
        }

        [Fact]
        public void TryLogin_Should_ReturnUserAndTrye()
        {
            // Arrange
            AuthService authService = GetAuthService();
            ApplicationUser NewUser = GetMockUser();
            string email = NewUser.Email;
            string password = NewUser.Password;

            // Act
            _ = authService.Register(NewUser);
            var result = authService.TryLogin(email, password, out ApplicationUser user);

            // Assert
            Assert.True(result);
            Assert.NotNull(user);
            Assert.NotEqual("", user.Name);
        }

        [Fact]
        public void TryLogin_WithBadCridentials_Should_ReturnFalse()
        {
            // Arrange
            AuthService authService = GetAuthService();
            ApplicationUser NewUser = GetMockUser();

            // Act
            _ = authService.Register(NewUser);
            var result = authService.TryLogin(NewUser.Email, NewUser.Password + "BAD", out _);


            // Assert
            Assert.False(result);
        }


        [Fact]
        public void TryLogin_EmailCaseInsenitive_Should_ReturnOk()
        {
            // Arrange
            AuthService authService = GetAuthService();
            ApplicationUser NewUser = GetMockUser();
            ApplicationUser NewToRegister = GetMockUser();

            // Act
            _ = authService.Register(NewToRegister);
            bool resultUpperBool = authService.TryLogin(NewUser.Email.ToUpper(), NewUser.Password, out ApplicationUser resultUpper);
            bool resultLowerBool = authService.TryLogin(NewUser.Email.ToLower(), NewUser.Password, out ApplicationUser resultLower);

            // Assert
            Assert.True(resultUpperBool);
            Assert.True(resultLowerBool);
            Assert.NotNull(resultUpper);
            Assert.NotNull(resultLower);
        }
        #endregion

        #region register

        [Fact]
        public void Register_Should_HashPassword()
        {
            // Arrange
            AuthService d = GetAuthService();
            ApplicationUser NewUser = GetMockUser();

            // Act
            var result = d.Register(NewUser);

            // Assert
            Assert.NotEqual("password", result.Password);
        }

        [Fact]
        public void Register_Should_IncreaseUserCount()
        {
            // Arrange
            AuthService d = GetAuthService();
            ApplicationUser NewUser = GetMockUser();
            int excpected;
            int actual;

            // Act
            excpected = d.Users.Count();
            _ = d.Register(NewUser);
            actual = d.Users.Count();

            // Assert
            Assert.NotEqual(excpected, actual);
        }

        [Fact]
        public void Register_Twise_Should_NotWork()
        {
            // Arrange
            AuthService d = GetAuthService();
            ApplicationUser NewUser = GetMockUser();
            int excpected;
            int actual;

            // Act
            _ = d.Register(NewUser);
            excpected = d.Users.Count();
            _ = d.Register(NewUser);
            actual = d.Users.Count();

            // Assert
            Assert.Equal(excpected, actual);
        }

        #endregion
    }
}
