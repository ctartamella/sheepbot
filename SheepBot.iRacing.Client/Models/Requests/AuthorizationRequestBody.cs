using System.Text.Json.Serialization;

namespace SheepBot.iRacing.Client.Models.Requests;

public record AuthorizationRequestBody
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; init; } = null!;
}