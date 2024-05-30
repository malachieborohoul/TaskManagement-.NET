using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Domain.DTOs.Request.Auth;

namespace TaskManagement.WebUI.Extensions;

public class CustomAuthenticationStateProvider(LocalStorageService localStorageService):AuthenticationStateProvider
{
    private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
    
    

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenModel = await localStorageService.GetModelFromToken();
        if (string.IsNullOrEmpty(tokenModel.Token)) return await Task. FromResult(new AuthenticationState (anonymous));
        var getUserClaims = DecryptToken(tokenModel.Token!);
        if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));
        var claimsPrincipal = SetClaimPrincipal(getUserClaims);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }
    
    

    public async Task UpdateAuthenticationState(LocalStorageDTO localStorageDTO)
    {
        var claimsPrincipal = new ClaimsPrincipal();
        if (localStorageDTO.Token != null || localStorageDTO.Refresh != null)
        {
            await localStorageService.SetBrowserLocalStorage (localStorageDTO); 
            var getUserClaims = DecryptToken(localStorageDTO.Token!); 
            claimsPrincipal = SetClaimPrincipal(getUserClaims);
           
        }
        else
        {
            await localStorageService.RemoveTokenFromBrowserLocalStroage();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }        
    }



    public static ClaimsPrincipal SetClaimPrincipal(UserClaimsDto claims)
    {
        if (claims.Email is null) return new ClaimsPrincipal();
        return new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim ("Id", claims.Id),

            new(ClaimTypes.Name, claims.UserName!), 
            new(ClaimTypes.Email, claims.Email!),
            new(ClaimTypes.Role, claims.Role!),
            new Claim ("Fullname", claims.Fullname)
        ], Constant.AuthenticationType));

    }
    
    

    private static UserClaimsDto DecryptToken(string jwtToken)
    {
        try
        {
            if (string.IsNullOrEmpty(jwtToken)) return new UserClaimsDto();
            var handler = new JwtSecurityTokenHandler(); 
            var token = handler.ReadJwtToken(jwtToken);
            var name = token.Claims.FirstOrDefault(_ => _.Type== ClaimTypes.Name)! .Value;
            var email = token.Claims. FirstOrDefault(_ => _.Type == ClaimTypes.Email)! .Value;
            var role = token.Claims. FirstOrDefault(_ => _. Type == ClaimTypes.Role)! .Value;
            var fullname = token.Claims. FirstOrDefault(_ => _.Type == "Fullname")!.Value; 
            var id = token.Claims. FirstOrDefault(_ => _.Type == "Id")!.Value;
            return new UserClaimsDto(Id: id, Fullname: fullname, UserName: name, Email: email, Role: role);

        }
        catch
        {
            return null!;
        }        
    }    
}    