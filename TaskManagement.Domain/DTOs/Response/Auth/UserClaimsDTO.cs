namespace TaskManagement.Application.DTOs.Response;

public record UserClaimsDto(string Id = null!,string Fullname = null!, string UserName = null!,string Email = null!,string Role = null! );
