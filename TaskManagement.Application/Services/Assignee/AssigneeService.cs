using System.Net.Http.Json;
using TaskManagement.Application.DTOs.Response.SubTask;
using TaskManagement.Domain.Entity.Tasks;
using TaskManagement.Application.DTOs.Response;
using TaskManagement.Application.Extensions;

namespace TaskManagement.Application.Services;

public class AssigneeService(HttpClientService httpClientService):IAssigneeService
{
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }
    public async Task<GeneralResponse> DeleteAssigneeAsync(Guid taskId, string userId)
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.DeleteAsync($"{Constant.DeleteAssigneeRoute}/{taskId}/{userId}");
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "Assignee deleted successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }

    public async Task<GeneralResponse> GetAssigneeAsync(Guid taskId, string userId)
    {
        var privateClient = await httpClientService.GetPrivateClient();
        var response = await privateClient.DeleteAsync($"{Constant.DeleteAssigneeRoute}/{taskId}/{userId}");
        string error = CheckResponseStatus(response);
        if (!string.IsNullOrEmpty(error))
            return new GeneralResponse(Flag: false, Message: error);
        
        var result = new GeneralResponse(true, "Assignee exist");
        return result!;
    }
    
    public async Task<IEnumerable<Assignee>> GetAssigneesByTaskIdAsync(Guid taskId)
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.GetAsync($"{Constant.DeleteAssigneeRoute}/{taskId}"); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Assignee>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}