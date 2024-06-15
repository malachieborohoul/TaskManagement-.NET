using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entities.Authentication;

namespace DuendeServer.Services;

public class ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager):IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string sub = context.Subject.GetSubjectId();
        ApplicationUser user = await userManager.FindByIdAsync(sub);
        ClaimsPrincipal userClaims = await userClaimsPrincipalFactory.CreateAsync(user);
        List<Claim> claims = userClaims.Claims.ToList();
        claims = claims.Where(u => context.RequestedClaimTypes.Contains(u.Type)).ToList();
        claims.Add(new Claim(JwtClaimTypes.Name, user.Name));

        if (userManager.SupportsUserRole)
        {
            IList<string> roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }

        context.IssuedClaims = claims;

    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string sub = context.Subject.GetSubjectId();

        ApplicationUser user = await userManager.FindByIdAsync(sub);

        context.IsActive = user != null;
    }
}