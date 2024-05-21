using System.Net.Http.Json;
using Application.DTOs.Response;
using Application.Extensions;
using Domain.Entity.Tasks;

namespace Application.Services;

public class RoleService(HttpClientService httpClientService):IRoleService
{
    
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }
    public async Task<IEnumerable<GetRoleDTO>> GetRolesAsync()
    {
        
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.GetAsync(Constant.GetRolesRoute); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetRoleDTO>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}