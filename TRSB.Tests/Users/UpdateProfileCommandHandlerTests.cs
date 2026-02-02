using FluentAssertions;
using Moq;
using TRSB.Domain.Entities;
using TRSB.Domain.Interfaces;
using TRSB.Tests.Repository;

namespace TRSB.Tests.Users
{
    public class UpdateProfileCommandHandlerTests
    {
        [Fact]
        public async Task Should_update_profile_successfully()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var existingUser = new User(
                "oldUser",
                "Old Name",
                "old@email.com",
                "old-hash"
            );

            var userRepository = new Mock<IUserRepository>();
            userRepository
                .Setup(r => r.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);

            userRepository
                .Setup(r => r.UpdateAsync(existingUser))
                .Returns(Task.CompletedTask);

            var passwordPolicy = new Mock<IPasswordPolicy>();
            passwordPolicy.Setup(p => p.Validate(It.IsAny<string>()));

            var passwordHasher = new Mock<IPasswordHasher>();
            passwordHasher
                .Setup(h => h.Hash(It.IsAny<string>()))
                .Returns("new-hash");

            var handler = new UpdateProfileCommandHandler(
                userRepository.Object,                
                passwordHasher.Object,
                passwordPolicy.Object
            );

            var command = new UpdateProfileCommand(
                userId,
                "newUser",
                "new@email.com"
            );

            // Act
            await handler.Handle(command, default);

            // Assert
            existingUser.Username.Should().Be("newUser");
            existingUser.Email.Should().Be("new@email.com");
            userRepository.Verify(r => r.UpdateAsync(existingUser), Times.Once);
        }

        [Fact]
        public async Task Should_throw_exception_when_user_not_found()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((User?)null);

            var passwordPolicy = new Mock<IPasswordPolicy>();
            var passwordHasher = new Mock<IPasswordHasher>();

            var handler = new UpdateProfileCommandHandler(
                userRepository.Object,                
                passwordHasher.Object,
                passwordPolicy.Object
            );

            var command = new UpdateProfileCommand(
                Guid.NewGuid(),
                "username",
                "email@test.com"
            );

            // Act
            Func<Task> act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Should_update_profile_using_fake_repository()
        {
            var repo = new FakeUserRepository();

            var user = new User(
                "user",
                "Name",
                "email@test.com",
                "hash"
            );

            await repo.AddAsync(user);

            var passwordPolicy = new Mock<IPasswordPolicy>();
            passwordPolicy.Setup(p => p.Validate(It.IsAny<string>()));

            var passwordHasher = new Mock<IPasswordHasher>();
            passwordHasher.Setup(h => h.Hash(It.IsAny<string>()))
                          .Returns("hashed");

            var handler = new UpdateProfileCommandHandler(
                repo,
                passwordHasher.Object,
                passwordPolicy.Object
                
            );

            var command = new UpdateProfileCommand(
                user.Id,
                "updatedUser",
                "updated@email.com"
            );

            await handler.Handle(command, default);

            var updated = await repo.GetByIdAsync(user.Id);

            updated!.Username.Should().Be("updatedUser");
        }

    }
}
