using MediatR;
using TRSB.Domain.Interfaces;

public class UpdateProfileCommandHandler
    : IRequestHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IPasswordPolicy _policy;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher hasher,
        IPasswordPolicy policy)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _policy = policy;
    }

    public async Task Handle(
        UpdateProfileCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId)
            ?? throw new ArgumentException("User not found");

        if (await _userRepository.ExistsByUsernameAsync(request.UserName, request.UserId))
            throw new ApplicationException("Username already exists");

        _policy.Validate(request.Password);

        var hash = _hasher.Hash(request.Password);

        user.UpdateProfile(user.Name, request.UserName, user.Email, hash);

        await _userRepository.UpdateAsync(user);
    }
}
