using Microsoft.EntityFrameworkCore;
using TRSB.Domain.Entities;
using TRSB.Domain.Interfaces;

namespace TRSB.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameOrEmailAsync(string value)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Username.ToLower() == value.ToLower() || 
                u.Email.ToLower() == value.ToLower());

    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, Guid ignoreUserId)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username && u.Id != ignoreUserId);
    }

    public async Task<bool> ExistsByEmailAsync(string email, Guid ignoreUserId)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email && u.Id != ignoreUserId);
    }


}
