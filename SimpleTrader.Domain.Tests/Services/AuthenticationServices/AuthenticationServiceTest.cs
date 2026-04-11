using Moq;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AthenticationService;
using static SimpleTrader.Domain.Services.AthenticationService.IAuthenticationService;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    internal class AuthenticationServiceTest
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthenticationService _authService;

        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);

        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUserName_ReturnAccountForCorrectUserName()
        {
            //[MethodName]_[Scenario]_[Result]
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";

            _mockAccountService.Setup(x => x.GetByUsername(expectedUsername)).ReturnsAsync(new Account
            {
                AccountHolder = new User { Username = expectedUsername },
                Balance = 1000
            });

            _mockPasswordHasher.Setup(x => x.Verify(password, It.IsAny<string>())).Returns(true);

            //Act
            Account account = await _authService.Login(expectedUsername, password);

            //Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUserName_ThrowsInvalidPasswordExceptionForUserName()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";

            _mockAccountService.Setup(x => x.GetByUsername(expectedUsername)).ReturnsAsync(new Account
            {
                AccountHolder = new User { Username = expectedUsername },
                Balance = 1000
            });

            _mockPasswordHasher.Setup(x => x.Verify(password, It.IsAny<string>())).Returns(false);

            //Act
            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(async () => await _authService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Login_WithNonExistingUserName_ThrowsInvalidPasswordExceptionForUserName()
        {
            // Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";


            _mockPasswordHasher.Setup(x => x.Verify(password, It.IsAny<string>())).Returns(false);

            //Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(async () => await _authService.Login(expectedUsername, password));

            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            // Arrange
            string password = "testpassword";
            string confirmPassword = "differentpassword";

            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;

            // Act
            RegistrationResult result = await _authService.Register("email@example.com", "username", password, confirmPassword);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task Register_WithExistingEmail_ReturnsEmailAlreadyExists()
        {
            // Arrange
            string email = "test@example.com";
            _mockAccountService.Setup(x => x.GetByEmail(email)).ReturnsAsync(new Account());

            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            // Act
            RegistrationResult result = await _authService.Register(email, "username", "password", "password");

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task Register_WithExistingUsername_ReturnsUsernameAlreadyExists()
        {
            // Arrange
            string username = "testuser";
            _mockAccountService.Setup(x => x.GetByUsername(username)).ReturnsAsync(new Account());
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            // Act
            RegistrationResult result = await _authService.Register("email@example.com", username, "password", "password");
            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            // Arrange
            RegistrationResult expected = RegistrationResult.Success;

            // Act
            RegistrationResult result = await _authService.Register("email@example.com", "username", "password", "password");
            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}