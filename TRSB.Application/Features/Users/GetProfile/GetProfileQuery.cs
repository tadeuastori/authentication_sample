using MediatR;
using TRSB.Application.Dtos;

namespace TRSB.Application.Features.Users.GetProfile
{
    public record GetProfileQuery(Guid UserId)
        : IRequest<UserProfileDto>;
}
