using MediatR;
using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Domain.Entities;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Application.Track.QueryHandlers;

public class GetTrackConfigsForTrackHandler(SheepContext context) : IRequestHandler<GetTrackConfigsForTrack, IEnumerable<TrackConfig>>
{
    public async Task<IEnumerable<TrackConfig>> Handle(GetTrackConfigsForTrack request, CancellationToken cancellationToken)
    {
        var track = await context.Tracks
            .Include(track => track.TrackConfigs)
            .FirstAsync(t => t.Id == request.TrackId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return track.TrackConfigs;
    }
}