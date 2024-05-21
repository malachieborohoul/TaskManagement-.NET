using System.Net.Http.Json;
using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Application.DTOs.Response.User;
using Application.Extensions;
using Domain.Entity.Tasks;

namespace Application.Services;

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
            var privateClient = await httpClientService.GetPrivateClient();
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
            var privateClient = await httpClientService.GetPrivateClient();
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
}