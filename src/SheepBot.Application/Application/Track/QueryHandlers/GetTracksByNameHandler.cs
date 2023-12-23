using MediatR;
using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Application.Track.QueryHandlers;

// ReSharper disable once UnusedType.Global
public class GetTracksByNameHandler(SheepContext context) : IRequestHandler<GetTracksByName, IEnumerable<Domain.Entities.Track>>
{
    public async Task<IEnumerable<Domain.Entities.Track>> Handle(GetTracksByName request, CancellationToken cancellationToken)
    {
        var results = context.Tracks.Where(x => EF.Functions.Like(x.Name,$"%{request.Name}%"));

        if (request.MaxResults > 0)
        {
            results = results
                .OrderBy(x => x.Name)
                .Take(request.MaxResults);
        }
        
        return await results.ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}