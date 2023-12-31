using MediatR;
using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Application.Track.QueryHandlers;

// ReSharper disable once UnusedType.Global
public class GetTrackByIdHandler(SheepContext context) : IRequestHandler<GetTrackById, Domain.Entities.Track?>
{
    public async Task<Domain.Entities.Track?> Handle(GetTrackById request, CancellationToken cancellationToken)
    {
        return await context.Tracks
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}