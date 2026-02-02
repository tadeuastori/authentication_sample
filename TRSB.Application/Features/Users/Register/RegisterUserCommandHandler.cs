using MediatR;
using TRSB.Domain.Entities;
using TRSB.Domain.Interfaces;

namespace TRSB.Application.Features.Users.Register;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordPolicy _passwordPolicy;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUserRepository repository,
        IPasswordPolicy passwordPolicy,
        IPasswordHasher passwordHasher)
    {
        _userRepository = repository;
        _passwordPolicy = passwordPolicy;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _passwordPolicy.Validate(request.Password);

        var hash = _passwordHasher.Hash(request.Password);

        if (await _userRepository.ExistsByUsernameAsync(request.Username))
            throw new ApplicationException("Username already exists");

        if (await _userRepository.ExistsByEmailAsync(request.Email))
            throw new ApplicationException("Email already exists");

        var user = new User(
            request.Username,
            request.Name,
            request.Email,
            hash
        );

        await _userRepository.AddAsync(user);
        return user.Id;
    }
}
