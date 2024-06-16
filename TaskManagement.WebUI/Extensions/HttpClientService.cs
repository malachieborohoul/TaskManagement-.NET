
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace TaskManagement.WebUI.Extensions;



public class HttpClientService(IHttpClientFactory httpClientFactory ,IAccessTokenProvider accessTokenProvider
    //LocalStorageService localStorageService
    )
{
    private HttpClient CreateClient() => httpClientFactory!.CreateClient(Constant.HttpClientName);
    public HttpClient GetPublicClient() => CreateClient();
    public async Task<HttpClient> GetPrivateClient()
    {
        try
        {
            var client = CreateClient();
            /*
            var localStorageDTO = await localStorageService.GetModelFromToken();
            if (string.IsNullOrEmpty(localStorageDTO. Token))
                return client;
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(Constant.HttpClientHeaderScheme, localStorageDTO.Token);*/
            
            var tokenResult = await accessTokenProvider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            return client;
        }
        catch { return new HttpClient(); }

    }
}
