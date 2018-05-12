using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace SingleSignOn.SampleHost.Extensions
{
    public static class IdentityExtensions
    {
        public static UserRole ClaimUserType(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var identity2 = identity as ClaimsIdentity;
            string type = identity2 != null ? identity2.FindFirstValue(ClaimTypes.Role) : null;
            return (UserRole)System.Enum.Parse(typeof(UserRole), type);
        }

        public static string ClaimUsername(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var identity2 = identity as ClaimsIdentity;
            return identity2 != null ? identity2.FindFirstValue(ClaimTypes.Name) : null;
        }
    }
}