using TRSB.Domain.Entities;

namespace TRSB.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByUsernameOrEmailAsync(string value);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);

    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);

    Task<bool> ExistsByUsernameAsync(string username, Guid ignoreUserId);
    Task<bool> ExistsByEmailAsync(string email, Guid ignoreUserId);

}
