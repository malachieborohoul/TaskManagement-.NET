
using System.Net.Http.Json;
using TaskManagement.Domain.DTOs.Request.Auth;
using TaskManagement.Domain.DTOs.Request.User;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.User;
using TaskManagement.WebUI.Extensions;

namespace TaskManagement.WebUI.Services.User;

public class UserService(HttpClientService httpClientService):IUserService
{
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }
    public async Task<List<GetUsersWithRolesResponseDTO>> GetUsersAsync()
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.GetAsync(Constant.GetUsersRoute); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<List<GetUsersWithRolesResponseDTO>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GeneralResponse> CreateUserAsync(RegisterDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.PostAsJsonAsync(Constant.GetUsersRoute, model);
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
            var result =  new GeneralResponse(true, "User saved successfully");
            return result!;
            
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }

    public async Task<GeneralResponse> UpdateUserAsync(string userId, UpdateUserDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            

            var response = await privateClient.PutAsJsonAsync($"{Constant.GetUsersRoute}/{userId}",model );
            var error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "User updated successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }
    
    public async Task<GeneralResponse> DeleteUserAsync(GetUsersWithRolesResponseDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.DeleteAsync($"{Constant.GetUsersRoute}/{model.Id}");
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "User deleted successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }
}