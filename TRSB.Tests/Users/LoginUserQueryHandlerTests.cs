using TRSB.Domain.Entities;
using TRSB.Infrastructure.Security;
using TRSB.Tests.Repository;

namespace TRSB.Tests.Users;

public class LoginUserQueryHandlerTests
{
    [Fact]
    public async Task Should_login_with_valid_credentials()
    {
        // Arrange
        var repository = new FakeUserRepository();
        var hasher = new PasswordHasher();

        var password = "Test@123";
        var hash = hasher.Hash(password);

        var user = new User(
            "carlos",
            "Carlos",
            "carlos@test.com",
            hash
        );

        await repository.AddAsync(user);

        var handler = new LoginUserQueryHandler(repository, hasher);

        // Act
        var result = await handler.Handle(
            new LoginUserQuery("carlos", password),
            CancellationToken.None
        );

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
    }
}
