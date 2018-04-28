using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skillx.Modules.Auth.Core.Entities;

namespace Skillx.Modules.Auth.Data
{
    public class SkillxAuthContext : IdentityDbContext<User>
    {
        public SkillxAuthContext(DbContextOptions<SkillxAuthContext> options)
            : base(options) { }
    }
}
