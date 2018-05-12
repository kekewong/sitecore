using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;

namespace SingleSignOn.SampleHost.Services
{
    public interface IClaimsIdentityFactory
    {
        ClaimsIdentity CreateIdentity(UserDTO user);
    }

    public class ClaimsIdentityFactory : IClaimsIdentityFactory
    {
        public ClaimsIdentity CreateIdentity(UserDTO user)
        {
            var id = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, null);
            id.AddClaim(new Claim(ClaimTypes.PrimarySid, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String));

            return id;
        }
    }
}
