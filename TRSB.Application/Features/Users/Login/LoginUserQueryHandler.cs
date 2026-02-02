using MediatR;
using TRSB.Application.Dtos;
using TRSB.Domain.Interfaces;

public class LoginUserQueryHandler
    : IRequestHandler<LoginUserQuery, UserDto?>
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;

    public LoginUserQueryHandler(
        IUserRepository repo,
        IPasswordHasher hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<UserDto?> Handle(
        LoginUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _repo.GetByUsernameOrEmailAsync(request.Login);
        if (user == null) return null;

        if (!_hasher.Verify(request.Password, user.PasswordHash))
            return null;

        return new UserDto(
            user.Id,
            user.Username,
            user.Email
        );
    }
}
