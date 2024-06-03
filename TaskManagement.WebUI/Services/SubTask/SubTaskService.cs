
using System.Net.Http.Json;
using TaskManagement.Domain.DTOs.Request.SubTask;
using TaskManagement.Domain.DTOs.Response;
using TaskManagement.Domain.DTOs.Response.SubTask;
using TaskManagement.WebUI.Extensions;

namespace TaskManagement.WebUI.Services.SubTask;

public class SubTaskService(HttpClientService httpClientService):ISubTaskService
{
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }
    public async Task<IEnumerable<GetSubTaskDTO>> GetSubTasksByTaskIdAsync(Guid taskId)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.GetAsync($"{Constant.GetSubTasksRoute}/task/{taskId}"); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetSubTaskDTO>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GeneralResponse> CreateSubTaskAsync(CreateSubTaskDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.PostAsJsonAsync(Constant.GetSubTasksRoute, model);
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
            var result =  new GeneralResponse(true, "Sub Task saved successfully");
            return result!;
            
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }

    public async Task<GeneralResponse> DeleteSubTaskAsync(GetSubTaskDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            var response = await privateClient.DeleteAsync($"{Constant.GetSubTasksRoute}/{model.Id}");
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "Sub Task deleted successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }

    public async Task<GeneralResponse> UpdateSubTaskAsync(Guid subTaskId, UpdateSubTaskDTO model)
    {
        try
        {
            var privateClient =  httpClientService.GetPublicClient();

            

            var response = await privateClient.PutAsJsonAsync($"{Constant.GetSubTasksRoute}/{subTaskId}",model );
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "Sub Task updated successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }
}