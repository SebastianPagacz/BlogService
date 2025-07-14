using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace User.Domain.Repositories;

public class UserDataContext : IdentityDbContext<UserEntity, RoleEntity, string>
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }
}
