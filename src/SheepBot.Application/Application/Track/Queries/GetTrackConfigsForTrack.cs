using MediatR;
using SheepBot.Domain.Entities;

namespace SheepBot.Application.Application.Track.Queries;

public class GetTrackConfigsForTrack : IRequest<IEnumerable<TrackConfig>>
{
    public long TrackId { get; init; }
}