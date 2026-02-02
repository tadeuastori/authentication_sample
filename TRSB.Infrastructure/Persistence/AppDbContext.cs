using Microsoft.EntityFrameworkCore;
using TRSB.Domain.Entities;

namespace TRSB.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
