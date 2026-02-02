using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRSB.Domain.Entities;
using TRSB.Domain.Interfaces;

namespace TRSB.Tests.Repository
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task AddAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User?> GetByUsernameOrEmailAsync(string value)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(value, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Equals(value, StringComparison.OrdinalIgnoreCase)
            );

            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user)
        {
            // Como o objeto já está em memória, nada precisa ser feito
            return Task.CompletedTask;
        }

        public Task<bool> ExistsByUsernameAsync(string username)
        {
            return Task.FromResult(
                _users.Any(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            );
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            return Task.FromResult(
                _users.Any(u =>
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            );
        }

        public Task<bool> ExistsByUsernameAsync(string username, Guid ignoreUserId)
        {
            return Task.FromResult(
                _users.Any(u =>
                    u.Id != ignoreUserId &&
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            );
        }

        public Task<bool> ExistsByEmailAsync(string email, Guid ignoreUserId)
        {
            return Task.FromResult(
                _users.Any(u =>
                    u.Id != ignoreUserId &&
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
