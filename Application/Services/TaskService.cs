using System.Net.Http.Json;
using Application.DTOs.Request.Task;
using Application.DTOs.Response;
using Application.DTOs.Response.Task;
using Application.Extensions;
using Domain.Entity.Tasks;

namespace Application.Services;

public class TaskService(HttpClientService httpClientService):ITaskService
{
    
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }

    public async Task<IEnumerable<GetTaskDTO>> GetTasksAsync()
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.GetAsync(Constant.GetTasksRoute); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetTaskDTO>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GeneralResponse> CreateTaskAsync(CreateTaskDTO model)
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.PostAsJsonAsync(Constant.CreateTaskRoute, model);
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
            var result =  new GeneralResponse(true, "Task saved successfully");
            return result!;
            
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }
    
    public async Task<GeneralResponse> DeleteTaskAsync(GetTaskDTO model)
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.DeleteAsync($"{Constant.CreateTaskRoute}/{model.Id}");
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "Task deleted successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }

    public async Task<GeneralResponse> UpdateTaskAsync(Guid taskId, UpdateTaskDTO model)
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            

            var response = await privateClient.PutAsJsonAsync($"{Constant.CreateTaskRoute}/{taskId}",model );
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                return new GeneralResponse(Flag: false, Message: error);
        
            var result = new GeneralResponse(true, "Task updated successfully");
            return result!;
        }
        catch (Exception ex)
        {
            return new GeneralResponse(Flag: false, Message: ex.Message);
        }
    }
}