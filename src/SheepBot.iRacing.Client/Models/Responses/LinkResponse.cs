namespace SheepBot.iRacing.Client.Models.Responses;

public class LinkResponse
{
    public string Link { get; init; } = null!;
    public DateTime Expires { get; init; }
}