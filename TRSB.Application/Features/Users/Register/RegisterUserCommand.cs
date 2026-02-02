using MediatR;

namespace TRSB.Application.Features.Users.Register;

public record RegisterUserCommand(
    string Username,
    string Name,
    string Email,
    string Password
) : IRequest<Guid>;
