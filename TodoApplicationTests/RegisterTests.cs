
using CleanTodo.Application.UseCase;
using CleanTodo.Application.Validators;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using CleanTodo.Domain.DTOS;
using FluentValidation;
using Moq;

namespace TodoApplicationTests
{
    public class RegisterTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private RegisterUseCase _registerUseCase;
        private IValidator<RegisterDTO> _registerValidator;
        User user1 = new User { Id = Guid.NewGuid(), Username = "Test User 1", Password = "MotDePasseTest1234!" };
        RegisterDTO registerDto = new RegisterDTO { Username = "Test User 1", Password = "MotDePasseTest1234!" };

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _registerValidator = new RegisterValidation();
            _registerUseCase = new RegisterUseCase(_userRepositoryMock.Object, _registerValidator);

            _userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>())).ReturnsAsync(user1);
        }

        [Test]
        public async Task Register_ShouldReturnCreatedUser()
        {
            // Act
            var result = await _registerUseCase.Execute(registerDto);

            // Assert
            Assert.That(user1.Id == result.Id, "User is returned");
            Assert.That(user1.Username == result.Username, "Same username");
        }

        [Test]
        public async Task Register_ShouldCallRepositoryAddUserOnce()
        {
            // Act
            await _registerUseCase.Execute(registerDto);

            // Assert
            _userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

    }
}
