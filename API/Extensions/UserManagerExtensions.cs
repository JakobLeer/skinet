using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByClaimsPrincipalWithAddressAsync(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var appUser = await userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);
            return appUser;
        }

        public static async Task<AppUser> FindByClaimsPrincipal(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var appUser = await userManager.Users.SingleOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);
            return appUser;
        }
    }
}