using MediatR;
using TRSB.Application.Dtos;

public record LoginUserQuery(string Login, string Password)
    : IRequest<UserDto?>;
