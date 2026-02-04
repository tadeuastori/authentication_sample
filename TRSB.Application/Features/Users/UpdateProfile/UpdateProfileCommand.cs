using MediatR;

public record UpdateProfileCommand(
    Guid UserId,
    string UserName,
    string Password
) : IRequest;
