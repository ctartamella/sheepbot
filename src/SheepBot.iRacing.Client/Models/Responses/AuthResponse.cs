namespace SheepBot.iRacing.Client.Models.Responses;

internal class AuthResponse
{
    public string? AuthCode { get; init; }
    public string? AuthLoginSeries { get; init; }
    public string? AuthLoginToken { get; init; }
    public long CustId { get; init; }
    public string Email { get; init; } = null!;
    public string SsoCookieDomain { get; init; } = null!;
    public string SsoCookieName { get; init; } = null!;
    public string SsoCookiePath { get; init; } = null!;
    public string SsoCookieValue { get; init; } = null!;
}