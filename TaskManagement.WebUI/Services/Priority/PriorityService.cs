using System.Net.Http.Json;
using TaskManagement.WebUI.Extensions;

namespace TaskManagement.WebUI.Services.Priority;

public class PriorityService(HttpClientService httpClientService):IPriorityService
{
    
    private static string CheckResponseStatus(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return
                $"Sorry unknown error occured.{Environment.NewLine} Error Description:{Environment.NewLine} Status Code: .{response.StatusCode} .{Environment.NewLine} Reason Phrase: .{response.ReasonPhrase}";
        else
            return null;
    }
    public async Task<IEnumerable<Domain.Entity.Tasks.Priority>> GetPrioritiesAsync()
    {
        try
        {
            var privateClient = await httpClientService.GetPrivateClient();
            var response = await privateClient.GetAsync(Constant.GetPrioritiesRoute); 
            string error = CheckResponseStatus(response);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Domain.Entity.Tasks.Priority>>(); 
            return result!;
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}