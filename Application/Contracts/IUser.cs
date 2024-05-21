using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Application.DTOs.Response.User;
using Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts;

public interface IUser
{
    Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
    public Task<GeneralResponse> RegisterAsync(RegisterDTO model);

    public Task CreateAdmin();
    public  Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
}