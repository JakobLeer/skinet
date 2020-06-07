using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return email;
        }
    }
}