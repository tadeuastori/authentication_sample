using MediatR;
using TRSB.Application.Dtos;
using TRSB.Domain.Interfaces;

namespace TRSB.Application.Features.Users.GetProfile
{
    public class GetProfileQueryHandler
        : IRequestHandler<GetProfileQuery, UserProfileDto>
    {
        private readonly IUserRepository _userRepository;

        public GetProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileDto> Handle(
            GetProfileQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                throw new Exception("User not found");

            return new UserProfileDto
            {
                Id = user.Id,
                UserName = user.Username,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
