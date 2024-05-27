namespace TaskManagement.Domain.DTOs.Request.Auth;

public class LocalStorageDTO
{
    public string? Token { get; set; }
    public string? Refresh { get; set; }
}