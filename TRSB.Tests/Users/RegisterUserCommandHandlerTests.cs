using MediatR;
using TRSB.Application.Features.Users.Register;
using TRSB.Domain.Entities;
using TRSB.Domain.Interfaces;
using TRSB.Infrastructure.Security;
using TRSB.Tests.Repository;

namespace TRSB.Tests.Users;

public class RegisterUserCommandHandlerTests
{
    private readonly FakeUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordPolicy _passwordPolicy;

    public RegisterUserCommandHandlerTests()
    {
        _repository = new FakeUserRepository();
        _passwordHasher = new PasswordHasher();
        _passwordPolicy = new ConfigurablePasswordPolicy(
            minLength: 6,
            minSpecialChars: 1
        );
    }

    [Fact]
    public async Task Should_register_user_successfully()
    {
        // Arrange
        var handler = new RegisterUserCommandHandler(
            _repository,            
            _passwordPolicy,
            _passwordHasher
        );

        var command = new RegisterUserCommand(
            "carlos",
            "Carlos Astori",
            "carlos@test.com",
            "Test@123"
        );

        // Act
        var userId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var user = await _repository.GetByIdAsync(userId);
        Assert.NotNull(user);
        Assert.Equal("carlos", user!.Username);
    }

    [Fact]
    public async Task Should_fail_if_username_already_exists()
    {
        // Arrange
        await _repository.AddAsync(
            new User(
                "carlos",
                "Carlos",
                "carlos@test.com",
                "hash"
            )
        );

        var handler = new RegisterUserCommandHandler(
            _repository,            
            _passwordPolicy,
            _passwordHasher
        );

        var command = new RegisterUserCommand(
            "carlos",
            "Another",
            "another@test.com",
            "Test@123"
        );

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}
