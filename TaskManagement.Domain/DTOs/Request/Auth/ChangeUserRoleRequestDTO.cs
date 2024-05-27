namespace TaskManagement.Domain.DTOs.Request.Auth;

public record ChangeUserRoleRequestDTO(string UserEmail, string RoleName);