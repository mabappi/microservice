using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.IdentityProvider.Application.Data
{
    public sealed class IdentityProviderDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityProviderDbContext(DbContextOptions<IdentityProviderDbContext> options) : base(options)
        {
        }
    }
}
