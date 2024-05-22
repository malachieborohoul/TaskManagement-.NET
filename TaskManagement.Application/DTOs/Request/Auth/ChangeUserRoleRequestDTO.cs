namespace TaskManagement.Application.DTOs.Request.Account;

public record ChangeUserRoleRequestDTO(string UserEmail, string RoleName);