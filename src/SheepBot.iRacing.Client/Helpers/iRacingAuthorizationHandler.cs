using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using SheepBot.Domain.Config;
using SheepBot.iRacing.Client.Models.Requests;

namespace SheepBot.iRacing.Client.Helpers;

// ReSharper disable once InconsistentNaming
internal class iRacingAuthorizationHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _email;
    private readonly string _encodedPassword;
    
    public iRacingAuthorizationHandler(IHttpClientFactory httpClientFactory, iRacingSettings settings, HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
        ArgumentNullException.ThrowIfNull(settings);

        _httpClientFactory = httpClientFactory;
        _email = settings.Email;
        _encodedPassword = EncodePassword(_email, settings.Password);
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var body = new AuthorizationRequestBody
        {
            Email = _email,
            Password = _encodedPassword
        };

        using var client = _httpClientFactory.CreateClient("NoAuth");

        var bodyString = JsonSerializer.Serialize(body);
        var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
        
        var authResponse = await client.PostAsync("auth", content, cancellationToken).ConfigureAwait(false);
        if (!authResponse.IsSuccessStatusCode)
        {
            throw new InvalidDataException();
        }

        // Copy headers into the original request.
        var headers = authResponse.Headers.GetValues("Set-Cookie");
        request.Headers.Add("Cookie", headers);
        
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private static string EncodePassword(string email, string password)
    {
        var bytes = Encoding.UTF8.GetBytes($"{password}{email}");
        var hashed = SHA256.HashData(bytes);

        return Convert.ToBase64String(hashed);
    }
}