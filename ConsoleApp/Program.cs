// discover endpoints from metadata

using System.Text.Json;
using IdentityModel.Client;

var client = new HttpClient();

var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

// request token
var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "ResourceOwnerPasswordClient",
    ClientSecret = "secret",
    Scope = "api1",
    UserName = "AhmedTurky",
    Password = "123456"
});
if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

Console.WriteLine(tokenResponse.Json);
Console.WriteLine("\n\n");


// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7260/WeatherForecast");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}
Console.ReadKey();