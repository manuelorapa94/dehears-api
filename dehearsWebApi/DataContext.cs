using dehearsWebApi.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dehearsWebApi
{
    public class DataContext : IdentityDbContext<UserEntity, UserRoleEntity, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
    }
}
