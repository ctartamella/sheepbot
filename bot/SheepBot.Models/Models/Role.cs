
namespace SheepBot.Models;

public class Role : ModelBase
{
    public long DiscordId { get; set; }
    public string RoleName { get; set; } = default!;

    public IEnumerable<Series> Series { get; set; } = new List<Series>();
}