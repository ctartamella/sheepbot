using MediatR;
using SheepBot.iRacing.Client.Models;

namespace SheepBot.Application.Application.Track.Commands;

public record UpdateTracks : IRequest
{
    public IEnumerable<iRacingTrack> Tracks { get; init; } = null!;
}