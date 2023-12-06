namespace SheepBot.iRacing.Client.Helpers;

// ReSharper disable once InconsistentNaming
internal class iRacingHttpClient : HttpClient
{
    public iRacingHttpClient(HttpMessageHandler handler) : base(handler)
    {

    }
}