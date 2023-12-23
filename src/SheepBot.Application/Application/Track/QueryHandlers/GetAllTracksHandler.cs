using MediatR;
using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Application.Track.QueryHandlers;

// ReSharper disable once UnusedType.Global
public class GetAllTracksHandler(SheepContext context) : IRequestHandler<GetAllTracks, IEnumerable<Domain.Entities.Track>>
{
    public async Task<IEnumerable<Domain.Entities.Track>> Handle(GetAllTracks request, CancellationToken cancellationToken)
    {
        var temp = await context.Tracks
            .Where(t => !t.IsRetired)
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return temp;
    }
}