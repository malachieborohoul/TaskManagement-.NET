namespace TaskManagement.Domain.DTOs.Response.User;

public class GetUsersWithRolesResponseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string RoleName { get; set; }
    public string RoleId { get; set; }
}