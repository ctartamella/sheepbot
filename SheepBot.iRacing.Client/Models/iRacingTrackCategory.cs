using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SheepBot.iRacing.Client.Models;

[JsonConverter(typeof(JsonStringEnumConverter<iRacingTrackCategory>))]
// ReSharper disable once InconsistentNaming
public enum iRacingTrackCategory
{
    [EnumMember(Value = "oval")]
    Oval = 1,
    [EnumMember(Value = "road")]
    Road = 2,
    [EnumMember(Value = "dirt_oval")]
    DirtOval = 3,
    [EnumMember(Value = "dirt_road")]
    Rallycross = 4
}